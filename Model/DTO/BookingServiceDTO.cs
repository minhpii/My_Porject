using My_Pro.Model.Entity;

namespace My_Pro.Model.DTO
{
    public class BookingServiceDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int ServiceId { get; set; }
        public ServiceDTO? Services { get; set; }
    }
}
