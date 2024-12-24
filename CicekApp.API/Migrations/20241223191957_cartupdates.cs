using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CicekApp.API.Migrations
{
    /// <inheritdoc />
    public partial class cartupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flowers_category_categoryid",
                schema: "public",
                table: "flowers");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_customers_customerid",
                schema: "public",
                table: "orders");

            migrationBuilder.DropTable(
                name: "customers",
                schema: "public");

            migrationBuilder.DropPrimaryKey(
                name: "PK_category",
                schema: "public",
                table: "category");

            migrationBuilder.RenameTable(
                name: "category",
                schema: "public",
                newName: "categories",
                newSchema: "public");

            migrationBuilder.RenameColumn(
                name: "phonenumber",
                schema: "public",
                table: "users",
                newName: "postalcode");

            migrationBuilder.RenameColumn(
                name: "customerid",
                schema: "public",
                table: "orders",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_orders_customerid",
                schema: "public",
                table: "orders",
                newName: "IX_orders_userid");

            migrationBuilder.AddColumn<string>(
                name: "address",
                schema: "public",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                schema: "public",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                schema: "public",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone",
                schema: "public",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cartid",
                schema: "public",
                table: "flowers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "orderid",
                schema: "public",
                table: "flowers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "courierid",
                schema: "public",
                table: "deliveries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                schema: "public",
                table: "categories",
                column: "categoryid");

            migrationBuilder.CreateTable(
                name: "carts",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    totalamount = table.Column<decimal>(type: "numeric", nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    orderid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.id);
                    table.ForeignKey(
                        name: "FK_carts_orders_orderid",
                        column: x => x.orderid,
                        principalSchema: "public",
                        principalTable: "orders",
                        principalColumn: "orderid");
                    table.ForeignKey(
                        name: "FK_carts_users_userid",
                        column: x => x.userid,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_flowers_cartid",
                schema: "public",
                table: "flowers",
                column: "cartid");

            migrationBuilder.CreateIndex(
                name: "IX_flowers_orderid",
                schema: "public",
                table: "flowers",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_deliveries_courierid",
                schema: "public",
                table: "deliveries",
                column: "courierid");

            migrationBuilder.CreateIndex(
                name: "IX_carts_orderid",
                schema: "public",
                table: "carts",
                column: "orderid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_carts_userid",
                schema: "public",
                table: "carts",
                column: "userid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_deliveries_couriers_courierid",
                schema: "public",
                table: "deliveries",
                column: "courierid",
                principalSchema: "public",
                principalTable: "couriers",
                principalColumn: "courierid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flowers_carts_cartid",
                schema: "public",
                table: "flowers",
                column: "cartid",
                principalSchema: "public",
                principalTable: "carts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_flowers_categories_categoryid",
                schema: "public",
                table: "flowers",
                column: "categoryid",
                principalSchema: "public",
                principalTable: "categories",
                principalColumn: "categoryid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flowers_orders_orderid",
                schema: "public",
                table: "flowers",
                column: "orderid",
                principalSchema: "public",
                principalTable: "orders",
                principalColumn: "orderid");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_users_userid",
                schema: "public",
                table: "orders",
                column: "userid",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "userid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deliveries_couriers_courierid",
                schema: "public",
                table: "deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_flowers_carts_cartid",
                schema: "public",
                table: "flowers");

            migrationBuilder.DropForeignKey(
                name: "FK_flowers_categories_categoryid",
                schema: "public",
                table: "flowers");

            migrationBuilder.DropForeignKey(
                name: "FK_flowers_orders_orderid",
                schema: "public",
                table: "flowers");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_users_userid",
                schema: "public",
                table: "orders");

            migrationBuilder.DropTable(
                name: "carts",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_flowers_cartid",
                schema: "public",
                table: "flowers");

            migrationBuilder.DropIndex(
                name: "IX_flowers_orderid",
                schema: "public",
                table: "flowers");

            migrationBuilder.DropIndex(
                name: "IX_deliveries_courierid",
                schema: "public",
                table: "deliveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                schema: "public",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "address",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "city",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "country",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "phone",
                schema: "public",
                table: "users");

            migrationBuilder.DropColumn(
                name: "cartid",
                schema: "public",
                table: "flowers");

            migrationBuilder.DropColumn(
                name: "orderid",
                schema: "public",
                table: "flowers");

            migrationBuilder.DropColumn(
                name: "courierid",
                schema: "public",
                table: "deliveries");

            migrationBuilder.RenameTable(
                name: "categories",
                schema: "public",
                newName: "category",
                newSchema: "public");

            migrationBuilder.RenameColumn(
                name: "postalcode",
                schema: "public",
                table: "users",
                newName: "phonenumber");

            migrationBuilder.RenameColumn(
                name: "userid",
                schema: "public",
                table: "orders",
                newName: "customerid");

            migrationBuilder.RenameIndex(
                name: "IX_orders_userid",
                schema: "public",
                table: "orders",
                newName: "IX_orders_customerid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_category",
                schema: "public",
                table: "category",
                column: "categoryid");

            migrationBuilder.CreateTable(
                name: "customers",
                schema: "public",
                columns: table => new
                {
                    customerid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: true),
                    firstname = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customerid);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_flowers_category_categoryid",
                schema: "public",
                table: "flowers",
                column: "categoryid",
                principalSchema: "public",
                principalTable: "category",
                principalColumn: "categoryid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_customers_customerid",
                schema: "public",
                table: "orders",
                column: "customerid",
                principalSchema: "public",
                principalTable: "customers",
                principalColumn: "customerid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
