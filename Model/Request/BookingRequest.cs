using My_Pro.Model.Entity;

namespace My_Pro.Model.Request
{
    public class BookingRequest
    {
        public int RoomId { get; set; }
        public List<int>? ServiceId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
