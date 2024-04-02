using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static My_Pro.Data.Enum;

namespace My_Pro.Model.Config
{
    public class RoomConfig : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.Property(x => x.PricePerNight).HasColumnType("decimal(18,2)");

            builder.HasData(
                   new Room
                   {
                       Id = 1,
                       Name = "Phòng 101",
                       Description = "Phòng 101",
                       RoomTypeId = 1,
                       PricePerNight = 150,
                       Status = RoomStatus.ConPhong
                   },
                   new Room
                   {
                       Id = 2,
                       Name = "Phòng 102",
                       Description = "Phòng 102",
                       RoomTypeId = 1,
                       PricePerNight = 150,
                       Status = RoomStatus.ConPhong
                   },
                   new Room
                   {
                       Id = 3,
                       Name = "Phòng 103",
                       Description = "Phòng 103",
                       RoomTypeId = 1,
                       PricePerNight = 200,
                       Status = RoomStatus.ConPhong
                   },
                   new Room
                   {
                       Id = 4,
                       Name = "Phòng 104",
                       Description = "Phòng 104",
                       RoomTypeId = 1,
                       PricePerNight = 200,
                       Status = RoomStatus.ConPhong
                   },
                   new Room
                   {
                       Id = 5,
                       Name = "Phòng 201",
                       Description = "Phòng 201",
                       RoomTypeId = 2,
                       PricePerNight = 250,
                       Status = RoomStatus.ConPhong
                   },
                   new Room
                   {
                       Id = 6,
                       Name = "Phòng 202",
                       Description = "Phòng 202",
                       RoomTypeId = 2,
                       PricePerNight = 250,
                       Status = RoomStatus.ConPhong
                   },
                   new Room
                   {
                       Id = 7,
                       Name = "Phòng 203",
                       Description = "Phòng 203",
                       RoomTypeId = 2,
                       PricePerNight = 300,
                       Status = RoomStatus.ConPhong
                   },
                   new Room
                   {
                       Id = 8,
                       Name = "Phòng 301",
                       Description = "Phòng 301",
                       RoomTypeId = 3,
                       PricePerNight = 350,
                       Status = RoomStatus.ConPhong
                   },
                   new Room
                   {
                       Id = 9,
                       Name = "Phòng 302",
                       Description = "Phòng 302",
                       RoomTypeId = 3,
                       PricePerNight = 350,
                       Status = RoomStatus.ConPhong
                   },
                   new Room
                   {
                       Id = 10,
                       Name = "Phòng 401",
                       Description = "Phòng 401",
                       RoomTypeId = 4,
                       PricePerNight = 400,
                       Status = RoomStatus.ConPhong
                   });
        }
    }
}
