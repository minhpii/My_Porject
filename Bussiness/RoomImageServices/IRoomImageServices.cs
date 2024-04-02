namespace My_Pro.Bussiness.RoomImageServices
{
    public interface IRoomImageServices
    {
        Task<bool> AddImageToRoom(int roomId, IFormFile file);
        Task<bool> RemoveImageToRoom(int id);
    }
}
