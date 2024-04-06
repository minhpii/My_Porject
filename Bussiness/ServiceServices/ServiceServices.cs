using Microsoft.EntityFrameworkCore;
using My_Pro.Data;
using My_Pro.Model.DTO;
using My_Pro.Model.Entity;
using My_Pro.Model.Request;

namespace My_Pro.Bussiness.ServiceServices
{
    public class ServiceServices : IServiceServices
    {
        private readonly AppDbContext _appDbContext;
        public ServiceServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<bool> Create(ServiceRequest request)
        {
            var serviceType = await _appDbContext.ServiceTypes.Where(x => x.Id == request.ServiceTypeId && !x.IsDeleted).FirstOrDefaultAsync();
            if (serviceType == null)
            {
                throw new AggregateException($"ServiceTypeId {request.ServiceTypeId} Not Found!");
            }
            var service = new Service()
            {
                Name = request.Name,
                Description = request.Description,
                ServiceTypeId = request.ServiceTypeId,
                Price = request.Price,

            };
            await _appDbContext.Services.AddAsync(service);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            var service = await CheckById(id);
            service.IsDeleted = true;
            _appDbContext.Services.Update(service);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<ServiceDTO> GetById(int id)
        {
            var service = await CheckById(id);
            var serviceDTO = MapToServiceDTO(service);
            return serviceDTO;
        }

        public async Task<List<ServiceDTO>> GetList(string? keyword)
        {
            var query = _appDbContext.Services.Where(x => !x.IsDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.ToLower().Contains(keyword.ToLower()));
            }
            var serviceDTO = query.Select(s => MapToServiceDTO(s));
            return await serviceDTO.ToListAsync();
        }

        public async Task<bool> Update(int id, ServiceRequest request)
        {
            var service = await CheckById(id);
            var serviceType = await _appDbContext.ServiceTypes.Where(x => x.Id == request.ServiceTypeId && !x.IsDeleted).FirstOrDefaultAsync();
            if (serviceType == null)
            {
                throw new AggregateException($"ServiceTypeId {request.ServiceTypeId} Not Found!");
            }
            service.Name = request.Name;
            service.Description = request.Description;
            service.ServiceTypeId = request.ServiceTypeId;
            service.Price = request.Price;

            _appDbContext.Services.Update(service);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<Service> CheckById(int id)
        {
            var service = await _appDbContext.Services.Where(x => x.Id == id && !x.IsDeleted).FirstOrDefaultAsync();
            if (service == null)
            {
                throw new Exception($"ServiceId {id} Not Found!");
            }
            return service;
        }

        private static ServiceDTO MapToServiceDTO(Service service)
        {
            if (service == null)
                return null;
            return new ServiceDTO()
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                ServiceTypeId = service.ServiceTypeId,
                ServiceTypes = new ServiceTypeDTO()
                {
                    Id = service.ServiceTypeId,
                    Name = service.Name,
                    Description = service.Description
                },
                Price = service.Price,
            };
        }
    }
}
