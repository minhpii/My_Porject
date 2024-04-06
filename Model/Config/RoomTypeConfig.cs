using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Pro.Data;
using My_Pro.Model.Entity;

namespace My_Pro.Model.Config
{
    public class RoomTypeConfig : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.HasData(
                   new RoomType
                   {
                       Id = 1,
                       Name = "Phòng Đơn (Single Room)",
                       Description = "Phòng dành cho một người, thường có một giường đơn."
                   },
                   new RoomType
                   {
                       Id = 2,
                       Name = "Phòng Đôi (Double Room)",
                       Description = "Phòng dành cho hai người, thường có một giường đôi."
                   },
                   new RoomType
                   {
                       Id = 3,
                       Name = "Phòng Gia Đình (Family Room)",
                       Description = "Phòng dành cho một gia đình hoặc một nhóm người, thường có nhiều giường hoặc giường đôi lớn để phù hợp với nhiều người."
                   },
                   new RoomType
                   {
                       Id = 4,
                       Name = "Suite",
                       Description = "Một loại phòng cao cấp có diện tích lớn hơn, thường bao gồm khu vực sống và khu vực ngủ riêng biệt. Có thể có các tiện nghi cao cấp như phòng tắm riêng, phòng khách riêng, vv."
                   },
                   new RoomType
                   {
                       Id = 5,
                       Name = "Phòng Deluxe (Deluxe Room)",
                       Description = "Một phiên bản nâng cấp của phòng đơn hoặc phòng đôi, thường có các tiện nghi cao cấp hơn như không gian rộng rãi hơn, quang cảnh tốt hơn, vv."
                   },
                   new RoomType
                   {
                       Id = 6,
                       Name = "Phòng Executive",
                       Description = "Phòng dành cho những khách hàng có nhu cầu kinh doanh, thường có các tiện nghi làm việc như bàn làm việc và kết nối Internet cao cấp."
                   },
                   new RoomType
                   {
                       Id = 7,
                       Name = "Phòng Hướng Biển (Ocean View Room)",
                       Description = "Phòng có tầm nhìn ra biển, thường có cửa sổ hoặc ban công nhìn ra biển."
                   },
                   new RoomType
                   {
                       Id = 8,
                       Name = "Phòng Hướng Thành Phố (City View Room)",
                       Description = "Phòng có tầm nhìn ra thành phố, thường có cửa sổ hoặc ban công nhìn ra các tòa nhà và cảnh đô thị."
                   },
                   new RoomType
                   {
                       Id = 9,
                       Name = "Phòng Hợp Nhóm (Group Room)",
                       Description = "Phòng lớn có thể chứa một nhóm người, thường được sử dụng cho các đoàn du lịch hoặc hội nghị."
                   },
                   new RoomType
                   {
                       Id = 10,
                       Name = "Phòng Penthouse",
                       Description = "Loại phòng sang trọng ở trên cùng của khách sạn, thường có diện tích lớn, quang cảnh tốt nhất và các tiện nghi cao cấp như hồ bơi riêng, sân thượng riêng, vv."
                   });
        }
    }
}
