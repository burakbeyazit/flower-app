using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CicekApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageurl",
                schema: "public",
                table: "flowers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageurl",
                schema: "public",
                table: "flowers");
        }
    }
}
