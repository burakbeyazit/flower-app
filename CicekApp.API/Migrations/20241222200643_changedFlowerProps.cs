using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CicekApp.API.Migrations
{
    /// <inheritdoc />
    public partial class changedFlowerProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "flowertype",
                schema: "public",
                table: "flowers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "flowertype",
                schema: "public",
                table: "flowers",
                type: "text",
                nullable: true);
        }
    }
}
