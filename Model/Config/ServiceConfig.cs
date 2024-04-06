using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Pro.Model.Entity;
using static My_Pro.Data.Enum;

namespace My_Pro.Model.Config
{
    public class ServiceConfig : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.HasData(
                   new Service
                   {
                       Id = 1,
                       Name = "Buffet Sáng",
                       Description = "Một loạt các lựa chọn sáng",
                       Price = 15,
                       ServiceTypeId = 1
                   },
                   new Service
                   {
                       Id = 2,
                       Name = "Tiện ích Hồ Bơi", 
                       Description = "Truy cập vào hồ bơi của khách sạn", 
                       Price = 10, 
                       ServiceTypeId = 3
                   },
                   new Service 
                   {
                       Id = 3,
                       Name = "Massage Spa", 
                       Description = "Massage thư giãn tại spa của khách sạn", 
                       Price = 50, 
                       ServiceTypeId = 2
                   },
                   new Service 
                   {
                       Id = 4,
                       Name = "Thuê Phòng Họp", 
                       Description = "Thuê phòng họp cho các cuộc họp", 
                       Price = 100, 
                       ServiceTypeId = 4 
                   },
                   new Service 
                   {
                       Id = 5,
                       Name = "Dịch vụ vệ sinh phòng", 
                       Description = "Dịch vụ làm sạch hàng ngày cho phòng", 
                       Price = 20, 
                       ServiceTypeId = 5
                   },
                   new Service
                   {
                       Id = 6,
                       Name = "Đưa đón sân bay", 
                       Description = "Dịch vụ vận chuyển từ/đến sân bay", 
                       Price = 30, 
                       ServiceTypeId = 7
                   },
                   new Service 
                   {
                       Id = 7,
                       Name = "Truy cập Phòng Tập Gym", 
                       Description = "Truy cập vào phòng tập gym của khách sạn", 
                       Price = 15, 
                       ServiceTypeId = 3
                   },
                   new Service
                   {
                       Id = 8,
                       Name = "Dịch vụ Giặt là", 
                       Description = "Dịch vụ giặt ủi cho quần áo của khách", 
                       Price = 25, 
                       ServiceTypeId = 10
                   },
                   new Service
                   {
                       Id = 9,
                       Name = "Internet tốc độ cao", 
                       Description = "Truy cập internet tốc độ cao trong phòng",
                       Price = 5,
                       ServiceTypeId = 9 
                   },
                   new Service 
                   {
                       Id = 10,
                       Name = "Gói Tour Thành Phố", 
                       Description = "Gói tour tham quan thành phố", 
                       Price = 75, 
                       ServiceTypeId = 6
                   });
        }
    }
}
