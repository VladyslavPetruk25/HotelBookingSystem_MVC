using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelBookingSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoomType",
                columns: new[] { "RoomTypeId", "Capacity", "Description", "Name", "PricePerNight" },
                values: new object[,]
                {
                    { 1, 2, "Cozy room for one or two", "Standart", 1000m },
                    { 2, 4, "Large room with sea view", "Luxe", 2500m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[] { 1, "petrukvl450@gmail.com", "Vladyslav", "Petruk", "0677692752" });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "RoomId", "Flour", "RoomNumber", "RoomTypeId" },
                values: new object[,]
                {
                    { 1, 1, 101, 1 },
                    { 2, 2, 205, 2 }
                });

            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "BookingId", "CheckInDate", "CheckOutDate", "RoomId", "Status", "TotalCost", "UserId" },
                values: new object[] { 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "New", 4000m, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Booking",
                keyColumn: "BookingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "RoomId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Room",
                keyColumn: "RoomId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "RoomTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomType",
                keyColumn: "RoomTypeId",
                keyValue: 1);
        }
    }
}
