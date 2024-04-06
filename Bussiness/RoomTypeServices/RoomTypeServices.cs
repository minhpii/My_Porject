using Microsoft.EntityFrameworkCore;
using My_Pro.Data;
using My_Pro.Model.DTO;
using My_Pro.Model.Entity;
using My_Pro.Model.Request;

namespace My_Pro.Bussiness.RoomTypeServices
{
    public class RoomTypeServices : IRoomTypeServices
    {
        private readonly AppDbContext _appDbContext;
        public RoomTypeServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> Create(RoomTypeRequest request)
        {
            var roomType = new RoomType()
            {
                Name = request.Name,
                Description = request.Description
            };
            await _appDbContext.RoomTypes.AddAsync(roomType);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var roomType = await CheckById(id);
            roomType.IsDeleted = true;
            _appDbContext.RoomTypes.Update(roomType);

            var room = _appDbContext.Rooms.Where(x => x.RoomTypeId == roomType.Id);
            foreach (var r in room)
            {
                r.IsDeleted = true;
                _appDbContext.Rooms.Update(r);
            }
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<RoomTypeDTO> GetById(int id)
        {
            var roomType = await CheckById(id);
            var roomTypeDTO = MapToRoomTypeDTO(roomType);
            return roomTypeDTO;
        }
        public async Task<List<RoomTypeDTO>> GetList(string? keyword)
        {
            var query = _appDbContext.RoomTypes.Where(x => !x.IsDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            }
            var roomTypeDTO = query.Select(rt => MapToRoomTypeDTO(rt));
            return await roomTypeDTO.ToListAsync();
        }
        public async Task<bool> Update(int id, RoomTypeRequest request)
        {
            var roomType = await CheckById(id);
            roomType.Name = request.Name;
            roomType.Description = request.Description;
            _appDbContext.RoomTypes.Update(roomType);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        private async Task<RoomType> CheckById(int id)
        {
            var checkRoomType = await _appDbContext.RoomTypes.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
            if (checkRoomType == null)
            {
                throw new AggregateException($"RoomTypeId {id} Not Found!");
            }
            return checkRoomType;
        }
        private static RoomTypeDTO MapToRoomTypeDTO(RoomType roomType)
        {
            if (roomType == null)
                return null;
            return new RoomTypeDTO()
            {
                Id = roomType.Id,
                Name = roomType.Name,
                Description = roomType.Description,
            };
        }
    }
}
