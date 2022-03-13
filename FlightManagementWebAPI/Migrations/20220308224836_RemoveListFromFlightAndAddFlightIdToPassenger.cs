using Microsoft.EntityFrameworkCore.Migrations;

namespace FlightManagementWebAPI.Migrations
{
    public partial class RemoveListFromFlightAndAddFlightIdToPassenger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckedPassengersList",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "PassengersList",
                table: "Flights");

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Passengers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Passengers");

            migrationBuilder.AddColumn<string>(
                name: "CheckedPassengersList",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassengersList",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
