using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JasaDinnerClubBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_DinnerEvents_DinnerEventDinnerId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_DinnerEventDinnerId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DinnerEventDinnerId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "BookingCapacity",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DinnerId",
                table: "Bookings",
                column: "DinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_DinnerEvents_DinnerId",
                table: "Bookings",
                column: "DinnerId",
                principalTable: "DinnerEvents",
                principalColumn: "DinnerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_DinnerEvents_DinnerId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_DinnerId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookingCapacity",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "DinnerEventDinnerId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DinnerEventDinnerId",
                table: "Bookings",
                column: "DinnerEventDinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_DinnerEvents_DinnerEventDinnerId",
                table: "Bookings",
                column: "DinnerEventDinnerId",
                principalTable: "DinnerEvents",
                principalColumn: "DinnerId");
        }
    }
}
