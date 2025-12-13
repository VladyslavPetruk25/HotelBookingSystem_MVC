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
        public DbSet<User> Users { get; set; }
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
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Admin",
                    LastName = "System",
                    Email = "petrukvl450@gmail.com",
                    PhoneNumber = "000",
                    Role = "Admin"
                },
                new User
                {
                    UserId = 2,
                    FirstName = "Vladyslav",
                    LastName = "Petruk",
                    Email = "vladyslav.petruk.24@pnu.edu.ua",
                    PhoneNumber = "0677692752",
                    Role = "User"
                }
            );
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    RoomId = 1,
                    RoomNumber = 101,
                    Flour = 1,
                    RoomTypeId = 1,
                    Status = "Available"
                },
                new Room
                {
                    RoomId = 2,
                    RoomNumber = 205,
                    Flour = 2,
                    RoomTypeId = 2,
                    Status = "Available"
                }
            );
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    BookingId = 1,
                    RoomId = 1,
                    UserId = 1,
                    CheckInDate = new DateTime(2025, 12, 01),
                    CheckOutDate = new DateTime(2025, 12, 05),
                    Status = "New",
                    TotalCost = 4000m
                }
            );
        }
    }
}
