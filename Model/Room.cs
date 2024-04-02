using My_Pro.Data;
using static My_Pro.Data.Enum;

namespace My_Pro.Model
{
    public class Room : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType? RoomTypes { get; set; }
        public decimal PricePerNight { get; set; }
        public ICollection<RoomImage>? Images { get; set; }
        public RoomStatus Status { get; set; }
    }
}
