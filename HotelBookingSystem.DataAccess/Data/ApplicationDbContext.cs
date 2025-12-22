using HotelBookingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSystem.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomType> RoomType { get; set; } 
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RoomType>().HasData(
                new RoomType
                {
                    RoomTypeId = 1,
                    Name = "Standart",
                    Description = "Cozy room for one or two",
                    Capacity = 2,
                    PricePerNight = 1000m,
                    ImageUrl=""
                },
                new RoomType
                {
                    RoomTypeId = 2,
                    Name = "Luxe",
                    Description = "Large room with sea view",
                    Capacity = 4,
                    PricePerNight = 2500m,
                    ImageUrl = ""
                }
            );
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    RoomId = 1,
                    RoomNumber = 101,
                    Flour = 1,
                    RoomTypeId = 1
                },
                new Room
                {
                    RoomId = 2,
                    RoomNumber = 205,
                    Flour = 2,
                    RoomTypeId = 2
                }
            );
        }
    }
}
