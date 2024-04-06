using My_Pro.Model.DTO;
using My_Pro.Model.Request;

namespace My_Pro.Bussiness.ServiceServices
{
    public interface IServiceServices
    {
        Task<List<ServiceDTO>> GetList(string? keyword);
        Task<ServiceDTO> GetById(int id);
        Task<bool> Create(ServiceRequest request);
        Task<bool> Update(int id, ServiceRequest request);
        Task<bool> Delete(int id);
    }
}
