using Microsoft.EntityFrameworkCore;
using My_Pro.Bussiness.ServiceTypeServices;
using My_Pro.Data;
using My_Pro.Model.DTO;
using My_Pro.Model.Entity;
using My_Pro.Model.Request;

namespace My_Pro.Bussiness.ServiceServices
{
    public class ServiceTypeServices : IServiceTypeServices
    {
        private readonly AppDbContext _appDbContext;
        public ServiceTypeServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> Create(ServiceTypeRequest request)
        {
            var serviceType = new ServiceType()
            {
                Name = request.Name,
                Description = request.Description
            };
            await _appDbContext.AddAsync(serviceType);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> Delete(int id)
        {
            var serviceType = await CheckById(id);
            serviceType.IsDeleted = true;
            _appDbContext.ServiceTypes.Update(serviceType);

            var service = _appDbContext.Services.Where(x => x.ServiceTypeId == serviceType.Id);
            foreach (var item in service)
            {
                item.IsDeleted = true;
                _appDbContext.Services.Update(item);
            }
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        public async Task<ServiceTypeDTO> GetById(int id)
        {
            var serviceType = await CheckById(id);
            var serviceTypeDTO = MapToServiceTypeDTO(serviceType);
            return serviceTypeDTO;
        }
        public async Task<List<ServiceTypeDTO>> GetList(string? keyword)
        {
            var query = _appDbContext.ServiceTypes.Where(x => !x.IsDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            }
            var serviceTypeDTO = query.Select(st => MapToServiceTypeDTO(st));
            return await serviceTypeDTO.ToListAsync();
        }
        public async Task<bool> Update(int id, ServiceTypeRequest request)
        {
            var serviceType = await CheckById(id);
            serviceType.Name = request.Name;
            serviceType.Description = request.Description;
            _appDbContext.ServiceTypes.Update(serviceType);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
        private async Task<ServiceType> CheckById(int id)
        {
            var checkServiceType = await _appDbContext.ServiceTypes.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
            if (checkServiceType != null)
            {
                throw new AggregateException($"ServiceTypeId {id} Not Found!");
            }
            return checkServiceType;
        }
        private static ServiceTypeDTO MapToServiceTypeDTO(ServiceType serviceType)
        {
            if (serviceType == null)
                return null;
            return new ServiceTypeDTO()
            {
                Id = serviceType.Id,
                Name = serviceType.Name,
                Description = serviceType.Description,
            };
        }
    }
}
