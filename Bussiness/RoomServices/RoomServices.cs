using Microsoft.EntityFrameworkCore;
using My_Pro.Data;
using My_Pro.Model;
using My_Pro.Model.DTO;
using My_Pro.Model.Request;
using static My_Pro.Data.Enum;

namespace My_Pro.Bussiness.RoomImageServices
{
    public class RoomServices : IRoomServices
    {
        private readonly AppDbContext _appDbContext;
        public RoomServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> Create(RoomRequest request)
        {
            var roomType = await _appDbContext.RoomTypes.FirstOrDefaultAsync(x => x.Id == request.RoomTypeId && !x.IsDeleted);
            if (roomType == null)
            {
                throw new AggregateException($"RoomTypeId {request.RoomTypeId} Not Found!");
            }
            var room = new Room()
            {
                Name = request.Name,
                Description = request.Description,
                RoomTypeId = request.RoomTypeId,
                PricePerNight = request.PricePerNight,
                Status = RoomStatus.ConPhong,
            };
            await _appDbContext.Rooms.AddAsync(room);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var room = await CheckId(id);
            room.IsDeleted = true;
            _appDbContext.Rooms.Update(room);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<RoomDTO> GetById(int id)
        {
            var room = await CheckId(id);
            var roomDTO = MapToRoomDTO(room);
            return roomDTO;
        }
        public async Task<List<RoomDTO>> GetList(string? keyword)
        {
            var query = _appDbContext.Rooms.Include(rt => rt.RoomTypes)
                .Include(img => img.Images)
                .Where(x => !x.IsDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            }
            var roomDTO = query.Select(r => MapToRoomDTO(r));
            return await roomDTO.ToListAsync();
        }
        public async Task<bool> Update(int id, RoomRequest request)
        {
            var room = await CheckId(id);
            var roomType = await _appDbContext.RoomTypes.FirstOrDefaultAsync(x => x.Id == request.RoomTypeId && !x.IsDeleted);
            if (roomType == null)
            {
                throw new AggregateException($"RoomTypeId {request.RoomTypeId} Not Found!");
            }
            room.Name = request.Name;
            room.Description = request.Description;
            room.RoomTypeId = request.RoomTypeId;
            room.PricePerNight = request.PricePerNight;

            _appDbContext.Rooms.Update(room);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateRoomStatus(int id, RoomStatus status)
        {
            var room = await CheckId(id);
            room.Status = status;
            _appDbContext.Rooms.Update(room);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        private async Task<Room> CheckId(int id)
        {
            var checkRoom = await _appDbContext.Rooms.Include(rt => rt.RoomTypes)
                .Include(ri => ri.Images)
                .Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
            if (checkRoom == null)
            {
                throw new AggregateException($"RoomId {id} Not Found!");
            }
            return checkRoom;
        }
        private static RoomDTO MapToRoomDTO(Room room)
        {
            if (room == null)
                return null;
            return new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                RoomTypeId = room.RoomTypeId,
                RoomTypes = new RoomTypeDTO
                {
                    Id = room.RoomTypes.Id,
                    Name = room.RoomTypes.Name,
                    Description = room.RoomTypes.Description
                },
                Images = room.Images.Select(ri => new RoomImageDTO
                {
                    Id = ri.Id,
                    RoomId = ri.RoomId,
                    FileName = ri.FileName,
                    ImagePath = ri.ImagePath,
                }).ToList(),
                PricePerNight = room.PricePerNight,
                Status = room.Status,
            };
        }
    }
}