
using Microsoft.EntityFrameworkCore;
using My_Pro.Data;
using My_Pro.Model;

namespace My_Pro.Bussiness.RoomImageServices
{
    public class RoomImageServices : IRoomImageServices
    {
        private readonly AppDbContext _appDbContext;
        public RoomImageServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<bool> AddImageToRoom(int roomId, IFormFile file)
        {
            var room = await _appDbContext.Rooms.FirstOrDefaultAsync(x => x.Id == roomId && !x.IsDeleted);
            if (room == null)
            {
                throw new AggregateException($"RoomId {roomId} Not Found!");
            }
            var roomImage = new RoomImage
            {
                RoomId = roomId,
                FileName = file.FileName,
                ImagePath = "images/rooms/" + file.FileName
            };

            await _appDbContext.RoomImages.AddAsync(roomImage);

            var directoryPath = Path.Combine("images", "rooms");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var filePath = Path.Combine("images", "rooms", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> RemoveImageToRoom(int id)
        {
            var roomImage = await _appDbContext.RoomImages.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
            if (roomImage == null)
            {
                throw new AggregateException($"RoomImageId {id} Not Found!");
            }

            var filePath = Path.Combine("images", "rooms", roomImage.FileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            roomImage.IsDeleted = true;
            _appDbContext.RoomImages.Update(roomImage);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
