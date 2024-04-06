using My_Pro.Model.DTO;
using My_Pro.Model.Request;

namespace My_Pro.Bussiness.ServiceTypeServices
{
    public interface IServiceTypeServices
    {
        Task<List<ServiceTypeDTO>> GetList(string? keyword);
        Task<ServiceTypeDTO> GetById(int id);
        Task<bool> Create(ServiceTypeRequest request);
        Task<bool> Update(int id, ServiceTypeRequest request);
        Task<bool> Delete(int id);
    }
}
