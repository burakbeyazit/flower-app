using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CicekApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCartFlowersCollectionToCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "status",
                schema: "public",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "cart_flowers",
                schema: "public",
                columns: table => new
                {
                    cartid = table.Column<int>(type: "integer", nullable: false),
                    flowerid = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_flowers", x => new { x.cartid, x.flowerid });
                    table.ForeignKey(
                        name: "FK_cart_flowers_carts_cartid",
                        column: x => x.cartid,
                        principalSchema: "public",
                        principalTable: "carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_flowers_flowers_flowerid",
                        column: x => x.flowerid,
                        principalSchema: "public",
                        principalTable: "flowers",
                        principalColumn: "flowerid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cart_flowers_flowerid",
                schema: "public",
                table: "cart_flowers",
                column: "flowerid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_flowers",
                schema: "public");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "public",
                table: "orders",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
