using My_Pro.Model.Entity;

namespace My_Pro.Model.Request
{
    public class ServiceRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int ServiceTypeId { get; set; }
        public decimal Price { get; set; }
    }
}
