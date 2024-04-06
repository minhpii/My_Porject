using My_Pro.Data;

namespace My_Pro.Model.Entity
{
    public class BookingService : BaseEntity
    {
        public int BookingId { get; set; }
        public Booking Bookings { get; set; }
        public int ServiceId { get; set; }
        public Service Services { get; set; }
    }
}
