using My_Pro.Data;

namespace My_Pro.Model.Entity
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int ServiceTypeId { get; set; }
        public ServiceType? ServiceType { get; set; }
        public decimal Price { get; set; }
    }
}
