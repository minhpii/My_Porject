using My_Pro.Model.Entity;

namespace My_Pro.Model.DTO
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceTypeDTO? ServiceTypes { get; set; }
        public decimal Price { get; set; }
    }
}
