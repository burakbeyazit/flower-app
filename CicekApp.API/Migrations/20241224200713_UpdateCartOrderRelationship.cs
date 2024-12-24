using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CicekApp.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCartOrderRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_orders_orderid",
                schema: "public",
                table: "carts");

            migrationBuilder.AddColumn<int>(
                name: "cartid",
                schema: "public",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_carts_orders_orderid",
                schema: "public",
                table: "carts",
                column: "orderid",
                principalSchema: "public",
                principalTable: "orders",
                principalColumn: "orderid",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_carts_orders_orderid",
                schema: "public",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "cartid",
                schema: "public",
                table: "orders");

            migrationBuilder.AddForeignKey(
                name: "FK_carts_orders_orderid",
                schema: "public",
                table: "carts",
                column: "orderid",
                principalSchema: "public",
                principalTable: "orders",
                principalColumn: "orderid");
        }
    }
}
