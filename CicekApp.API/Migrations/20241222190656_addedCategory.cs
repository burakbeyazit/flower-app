using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CicekApp.API.Migrations
{
    /// <inheritdoc />
    public partial class addedCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoryid",
                schema: "public",
                table: "flowers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "category",
                schema: "public",
                columns: table => new
                {
                    categoryid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    categoryname = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.categoryid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_flowers_categoryid",
                schema: "public",
                table: "flowers",
                column: "categoryid");

            migrationBuilder.AddForeignKey(
                name: "FK_flowers_category_categoryid",
                schema: "public",
                table: "flowers",
                column: "categoryid",
                principalSchema: "public",
                principalTable: "category",
                principalColumn: "categoryid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flowers_category_categoryid",
                schema: "public",
                table: "flowers");

            migrationBuilder.DropTable(
                name: "category",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_flowers_categoryid",
                schema: "public",
                table: "flowers");

            migrationBuilder.DropColumn(
                name: "categoryid",
                schema: "public",
                table: "flowers");
        }
    }
}
