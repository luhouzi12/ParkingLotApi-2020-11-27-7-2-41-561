using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingLotApi.Migrations
{
    public partial class AddListOrdersInParkingLots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ParkingLotEntityName",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ParkingLotEntityName",
                table: "Orders",
                column: "ParkingLotEntityName");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ParkingLots_ParkingLotEntityName",
                table: "Orders",
                column: "ParkingLotEntityName",
                principalTable: "ParkingLots",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ParkingLots_ParkingLotEntityName",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ParkingLotEntityName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ParkingLotEntityName",
                table: "Orders");
        }
    }
}
