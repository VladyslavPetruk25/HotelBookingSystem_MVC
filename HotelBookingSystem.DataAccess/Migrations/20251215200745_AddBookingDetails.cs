using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Booking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Booking");
        }
    }
}
