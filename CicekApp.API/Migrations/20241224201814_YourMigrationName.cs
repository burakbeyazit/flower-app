using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CicekApp.API.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_carts_userid",
                schema: "public",
                table: "carts");

            migrationBuilder.CreateIndex(
                name: "IX_carts_userid",
                schema: "public",
                table: "carts",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_carts_userid",
                schema: "public",
                table: "carts");

            migrationBuilder.CreateIndex(
                name: "IX_carts_userid",
                schema: "public",
                table: "carts",
                column: "userid",
                unique: true);
        }
    }
}
