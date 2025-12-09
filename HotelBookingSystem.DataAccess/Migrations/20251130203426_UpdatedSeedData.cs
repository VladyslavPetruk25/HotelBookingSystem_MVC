using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName", "PhoneNumber", "Role" },
                values: new object[] { "Admin", "System", "000", "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "PhoneNumber", "Role" },
                values: new object[] { 2, "vladyslav.petruk.24@pnu.edu.ua", "Vladyslav", "Petruk", "0677692752", "User" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName", "PhoneNumber", "Role" },
                values: new object[] { "Vladyslav", "Petruk", "0677692752", "User" });
        }
    }
}
