using My_Pro.Model.DTO;
using My_Pro.Model.Request;
using static My_Pro.Data.Enum;

namespace My_Pro.Bussiness.RoomImageServices
{
    public interface IRoomServices
    {
        Task<List<RoomDTO>> GetList(string? keyword);
        Task<RoomDTO> GetById(int id);
        Task<bool> Create(RoomRequest request);
        Task<bool> Update(int id, RoomRequest request);
        Task<bool> UpdateRoomStatus(int id, RoomStatus status);
        Task<bool> Delete(int id);
    }
}
