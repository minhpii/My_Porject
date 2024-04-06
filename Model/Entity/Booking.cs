using My_Pro.Data;

namespace My_Pro.Model.Entity
{
    public class Booking : BaseEntity
    {
        public string UsersId { get; set; }
        public ApplicationUser? Users { get; set; }
        public int RoomId { get; set; }
        public Room? Rooms { get; set; }
        public DateTime CheckInDate{ get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<BookingService>? BookingServices { get; set;}
    }
}
