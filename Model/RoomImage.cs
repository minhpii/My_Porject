using My_Pro.Data;

namespace My_Pro.Model
{
    public class RoomImage : BaseEntity
    {
        public int RoomId { get; set; }
        public Room? Rooms { get; set; }
        public string FileName { get; set; }
        public string ImagePath { get; set; }
    }
}
