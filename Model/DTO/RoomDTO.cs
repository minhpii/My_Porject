using static My_Pro.Data.Enum;

namespace My_Pro.Model.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int RoomTypeId { get; set; }
        public RoomTypeDTO? RoomTypes { get; set; }
        public decimal PricePerNight { get; set; }
        public RoomStatus Status { get; set; }
        public ICollection<RoomImageDTO> Images { get; set; }
    }
}
