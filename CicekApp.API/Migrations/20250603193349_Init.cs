using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CicekApp.API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "public",
                columns: table => new
                {
                    categoryid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    categoryname = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.categoryid);
                });

            migrationBuilder.CreateTable(
                name: "couriers",
                schema: "public",
                columns: table => new
                {
                    courierid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    couriername = table.Column<string>(type: "text", nullable: true),
                    courierphone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_couriers", x => x.courierid);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "public",
                columns: table => new
                {
                    roleid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.roleid);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    userid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: true),
                    passwordhash = table.Column<string>(type: "text", nullable: true),
                    firstname = table.Column<string>(type: "text", nullable: true),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    lastonline = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    roleid = table.Column<int>(type: "integer", nullable: false),
                    statusmessage = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    city = table.Column<string>(type: "text", nullable: true),
                    postalcode = table.Column<string>(type: "text", nullable: true),
                    country = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userid);
                    table.ForeignKey(
                        name: "FK_users_roles_roleid",
                        column: x => x.roleid,
                        principalSchema: "public",
                        principalTable: "roles",
                        principalColumn: "roleid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "public",
                columns: table => new
                {
                    orderid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    orderdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    totalprice = table.Column<decimal>(type: "numeric", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    cartid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.orderid);
                    table.ForeignKey(
                        name: "FK_orders_users_userid",
                        column: x => x.userid,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

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
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_carts_users_userid",
                        column: x => x.userid,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deliveries",
                schema: "public",
                columns: table => new
                {
                    deliveryid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    deliverydate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deliveryaddress = table.Column<string>(type: "text", nullable: true),
                    deliverystatus = table.Column<string>(type: "text", nullable: true),
                    deliveryperson = table.Column<string>(type: "text", nullable: true),
                    courierid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliveries", x => x.deliveryid);
                    table.ForeignKey(
                        name: "FK_deliveries_couriers_courierid",
                        column: x => x.courierid,
                        principalSchema: "public",
                        principalTable: "couriers",
                        principalColumn: "courierid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_deliveries_orders_orderid",
                        column: x => x.orderid,
                        principalSchema: "public",
                        principalTable: "orders",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flowers",
                schema: "public",
                columns: table => new
                {
                    flowerid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    flowername = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "numeric", nullable: false),
                    stockquantity = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    imageurl = table.Column<string>(type: "text", nullable: true),
                    categoryid = table.Column<int>(type: "integer", nullable: false),
                    cartid = table.Column<int>(type: "integer", nullable: true),
                    orderid = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flowers", x => x.flowerid);
                    table.ForeignKey(
                        name: "FK_flowers_carts_cartid",
                        column: x => x.cartid,
                        principalSchema: "public",
                        principalTable: "carts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_flowers_categories_categoryid",
                        column: x => x.categoryid,
                        principalSchema: "public",
                        principalTable: "categories",
                        principalColumn: "categoryid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flowers_orders_orderid",
                        column: x => x.orderid,
                        principalSchema: "public",
                        principalTable: "orders",
                        principalColumn: "orderid");
                });

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
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_deliveries_courierid",
                schema: "public",
                table: "deliveries",
                column: "courierid");

            migrationBuilder.CreateIndex(
                name: "IX_deliveries_orderid",
                schema: "public",
                table: "deliveries",
                column: "orderid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flowers_cartid",
                schema: "public",
                table: "flowers",
                column: "cartid");

            migrationBuilder.CreateIndex(
                name: "IX_flowers_categoryid",
                schema: "public",
                table: "flowers",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_flowers_orderid",
                schema: "public",
                table: "flowers",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_userid",
                schema: "public",
                table: "orders",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_users_roleid",
                schema: "public",
                table: "users",
                column: "roleid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_flowers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "deliveries",
                schema: "public");

            migrationBuilder.DropTable(
                name: "flowers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "couriers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "carts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "public");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "public");
        }
    }
}
