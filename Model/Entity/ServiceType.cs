using My_Pro.Data;

namespace My_Pro.Model.Entity
{
    public class ServiceType : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
