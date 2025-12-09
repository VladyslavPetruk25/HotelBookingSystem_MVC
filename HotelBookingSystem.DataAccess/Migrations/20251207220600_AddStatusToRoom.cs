using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Room",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "Status",
                value: "Available");

            migrationBuilder.UpdateData(
                table: "Room",
                keyColumn: "RoomId",
                keyValue: 2,
                column: "Status",
                value: "Available");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Room");
        }
    }
}
