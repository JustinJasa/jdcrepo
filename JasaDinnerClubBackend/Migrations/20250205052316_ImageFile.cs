using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JasaDinnerClubBackend.Migrations
{
    /// <inheritdoc />
    public partial class ImageFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "DinnerEvents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "DinnerEvents");
        }
    }
}
