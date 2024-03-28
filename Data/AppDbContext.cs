using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using My_Pro.Model;
using My_Pro.Model.Config;

namespace My_Pro.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoomTypeConfig());
            modelBuilder.ApplyConfiguration(new RoomConfig());
        }
    }
}
