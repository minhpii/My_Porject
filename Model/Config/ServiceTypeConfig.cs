using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using My_Pro.Model.Entity;
using static My_Pro.Data.Enum;

namespace My_Pro.Model.Config
{
    public class ServiceTypeConfig : IEntityTypeConfiguration<ServiceType>
    {
        public void Configure(EntityTypeBuilder<ServiceType> builder)
        {
            builder.HasData(
                   new ServiceType
                   {
                       Id = 1,
                       Name = "Ăn uống",
                       Description = "Dịch vụ liên quan đến ẩm thực và nhà hàng.",
                   },
                   new ServiceType
                   {
                       Id = 2,
                       Name = "Giải trí",
                       Description = "Dịch vụ giải trí như karaoke, rạp chiếu phim, v.v.",
                   },
                   new ServiceType
                   {
                       Id = 3,
                       Name = "Tiện ích",
                       Description = "Các tiện ích như hồ bơi, phòng tập thể dục, spa.",
                   },
                   new ServiceType
                   {
                       Id = 4,
                       Name = "Dịch vụ kinh doanh",
                       Description = "Các dịch vụ hỗ trợ kinh doanh như phòng họp, dịch vụ in ấn.",
                   },
                   new ServiceType
                   {
                       Id = 5,
                       Name = "Dịch vụ phòng",
                       Description = "Các dịch vụ được cung cấp trực tiếp trong phòng như dịch vụ phòng ăn, room service.",
                   },
                   new ServiceType
                   {
                       Id = 6,
                       Name = "Dịch vụ du lịch",
                       Description = "Cung cấp dịch vụ đặt tour, hỗ trợ du lịch.",
                   },
                   new ServiceType
                   {
                       Id = 7,
                       Name = "Dịch vụ vận chuyển",
                       Description = "Cung cấp dịch vụ đưa đón sân bay, thuê xe.",
                   },
                   new ServiceType
                   {
                       Id = 8,
                       Name = "Dịch vụ hội nghị",
                       Description = "Các dịch vụ liên quan đến tổ chức hội nghị, sự kiện.",
                   },
                   new ServiceType
                   {
                       Id = 9,
                       Name = "Dịch vụ internet",
                       Description = "Dịch vụ internet",
                   },
                   new ServiceType
                   {
                       Id = 10,
                       Name = "Dịch vụ giặt là",
                       Description = "Dịch vụ giặt ủi quần áo của khách.",
                   });
        }
    }
}
