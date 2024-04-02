using My_Pro.Model.DTO;
using My_Pro.Model.Request;

namespace My_Pro.Bussiness.RoomTypeServices
{
    public interface IRoomTypeServices
    {
        Task<List<RoomTypeDTO>> GetList(string? keyword);
        Task<RoomTypeDTO> GetById(int id);
        Task<bool> Create(RoomTypeRequest request);
        Task<bool> Update(int id, RoomTypeRequest request);
        Task<bool> Delete(int id);
    }
}
