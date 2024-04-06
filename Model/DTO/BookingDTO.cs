namespace My_Pro.Model.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public UserDTO? Users { get; set; }
        public int RoomId { get; set; }
        public RoomDTO? Rooms { get; set; }
        public List<BookingServiceDTO>? BookingServices { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
