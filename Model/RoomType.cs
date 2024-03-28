using My_Pro.Data;

namespace My_Pro.Model
{
    public class RoomType : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Room>? Rooms { get; set; }
    }
}
