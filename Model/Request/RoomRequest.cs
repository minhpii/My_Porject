namespace My_Pro.Model.Entity
{
    public class RoomRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int RoomTypeId { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
