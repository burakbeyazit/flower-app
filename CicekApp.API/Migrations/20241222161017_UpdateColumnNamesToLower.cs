using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CicekApp.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnNamesToLower : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deliveries_orders_OrderId",
                schema: "public",
                table: "deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_customers_CustomerId",
                schema: "public",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_RoleId",
                schema: "public",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "StatusMessage",
                schema: "public",
                table: "users",
                newName: "statusmessage");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "public",
                table: "users",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "public",
                table: "users",
                newName: "phonenumber");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "public",
                table: "users",
                newName: "passwordhash");

            migrationBuilder.RenameColumn(
                name: "LastOnline",
                schema: "public",
                table: "users",
                newName: "lastonline");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "public",
                table: "users",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "public",
                table: "users",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "public",
                table: "users",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "public",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "public",
                table: "users",
                newName: "createdat");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "public",
                table: "users",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_users_RoleId",
                schema: "public",
                table: "users",
                newName: "IX_users_roleid");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "public",
                table: "roles",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "public",
                table: "roles",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                schema: "public",
                table: "orders",
                newName: "totalprice");

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "public",
                table: "orders",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                schema: "public",
                table: "orders",
                newName: "orderdate");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "public",
                table: "orders",
                newName: "customerid");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                schema: "public",
                table: "orders",
                newName: "orderid");

            migrationBuilder.RenameIndex(
                name: "IX_orders_CustomerId",
                schema: "public",
                table: "orders",
                newName: "IX_orders_customerid");

            migrationBuilder.RenameColumn(
                name: "StockQuantity",
                schema: "public",
                table: "flowers",
                newName: "stockquantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "public",
                table: "flowers",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "FlowerType",
                schema: "public",
                table: "flowers",
                newName: "flowertype");

            migrationBuilder.RenameColumn(
                name: "FlowerName",
                schema: "public",
                table: "flowers",
                newName: "flowername");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "public",
                table: "flowers",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "FlowerId",
                schema: "public",
                table: "flowers",
                newName: "flowerid");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                schema: "public",
                table: "deliveries",
                newName: "orderid");

            migrationBuilder.RenameColumn(
                name: "DeliveryStatus",
                schema: "public",
                table: "deliveries",
                newName: "deliverystatus");

            migrationBuilder.RenameColumn(
                name: "DeliveryPerson",
                schema: "public",
                table: "deliveries",
                newName: "deliveryperson");

            migrationBuilder.RenameColumn(
                name: "DeliveryDate",
                schema: "public",
                table: "deliveries",
                newName: "deliverydate");

            migrationBuilder.RenameColumn(
                name: "DeliveryAddress",
                schema: "public",
                table: "deliveries",
                newName: "deliveryaddress");

            migrationBuilder.RenameColumn(
                name: "DeliveryId",
                schema: "public",
                table: "deliveries",
                newName: "deliveryid");

            migrationBuilder.RenameIndex(
                name: "IX_deliveries_OrderId",
                schema: "public",
                table: "deliveries",
                newName: "IX_deliveries_orderid");

            migrationBuilder.RenameColumn(
                name: "Phone",
                schema: "public",
                table: "customers",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "public",
                table: "customers",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "public",
                table: "customers",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "public",
                table: "customers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "public",
                table: "customers",
                newName: "customerid");

            migrationBuilder.RenameColumn(
                name: "CourierPhone",
                schema: "public",
                table: "couriers",
                newName: "courierphone");

            migrationBuilder.RenameColumn(
                name: "CourierName",
                schema: "public",
                table: "couriers",
                newName: "couriername");

            migrationBuilder.RenameColumn(
                name: "CourierId",
                schema: "public",
                table: "couriers",
                newName: "courierid");

            migrationBuilder.AddForeignKey(
                name: "FK_deliveries_orders_orderid",
                schema: "public",
                table: "deliveries",
                column: "orderid",
                principalSchema: "public",
                principalTable: "orders",
                principalColumn: "orderid",
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

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_roleid",
                schema: "public",
                table: "users",
                column: "roleid",
                principalSchema: "public",
                principalTable: "roles",
                principalColumn: "roleid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deliveries_orders_orderid",
                schema: "public",
                table: "deliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_customers_customerid",
                schema: "public",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_roleid",
                schema: "public",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "statusmessage",
                schema: "public",
                table: "users",
                newName: "StatusMessage");

            migrationBuilder.RenameColumn(
                name: "roleid",
                schema: "public",
                table: "users",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "phonenumber",
                schema: "public",
                table: "users",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "passwordhash",
                schema: "public",
                table: "users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "lastonline",
                schema: "public",
                table: "users",
                newName: "LastOnline");

            migrationBuilder.RenameColumn(
                name: "lastname",
                schema: "public",
                table: "users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "isactive",
                schema: "public",
                table: "users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "firstname",
                schema: "public",
                table: "users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "public",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "createdat",
                schema: "public",
                table: "users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "userid",
                schema: "public",
                table: "users",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_users_roleid",
                schema: "public",
                table: "users",
                newName: "IX_users_RoleId");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "public",
                table: "roles",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "roleid",
                schema: "public",
                table: "roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "totalprice",
                schema: "public",
                table: "orders",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "status",
                schema: "public",
                table: "orders",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "orderdate",
                schema: "public",
                table: "orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "customerid",
                schema: "public",
                table: "orders",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "orderid",
                schema: "public",
                table: "orders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_customerid",
                schema: "public",
                table: "orders",
                newName: "IX_orders_CustomerId");

            migrationBuilder.RenameColumn(
                name: "stockquantity",
                schema: "public",
                table: "flowers",
                newName: "StockQuantity");

            migrationBuilder.RenameColumn(
                name: "price",
                schema: "public",
                table: "flowers",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "flowertype",
                schema: "public",
                table: "flowers",
                newName: "FlowerType");

            migrationBuilder.RenameColumn(
                name: "flowername",
                schema: "public",
                table: "flowers",
                newName: "FlowerName");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "public",
                table: "flowers",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "flowerid",
                schema: "public",
                table: "flowers",
                newName: "FlowerId");

            migrationBuilder.RenameColumn(
                name: "orderid",
                schema: "public",
                table: "deliveries",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "deliverystatus",
                schema: "public",
                table: "deliveries",
                newName: "DeliveryStatus");

            migrationBuilder.RenameColumn(
                name: "deliveryperson",
                schema: "public",
                table: "deliveries",
                newName: "DeliveryPerson");

            migrationBuilder.RenameColumn(
                name: "deliverydate",
                schema: "public",
                table: "deliveries",
                newName: "DeliveryDate");

            migrationBuilder.RenameColumn(
                name: "deliveryaddress",
                schema: "public",
                table: "deliveries",
                newName: "DeliveryAddress");

            migrationBuilder.RenameColumn(
                name: "deliveryid",
                schema: "public",
                table: "deliveries",
                newName: "DeliveryId");

            migrationBuilder.RenameIndex(
                name: "IX_deliveries_orderid",
                schema: "public",
                table: "deliveries",
                newName: "IX_deliveries_OrderId");

            migrationBuilder.RenameColumn(
                name: "phone",
                schema: "public",
                table: "customers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "lastname",
                schema: "public",
                table: "customers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstname",
                schema: "public",
                table: "customers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "public",
                table: "customers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "customerid",
                schema: "public",
                table: "customers",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "courierphone",
                schema: "public",
                table: "couriers",
                newName: "CourierPhone");

            migrationBuilder.RenameColumn(
                name: "couriername",
                schema: "public",
                table: "couriers",
                newName: "CourierName");

            migrationBuilder.RenameColumn(
                name: "courierid",
                schema: "public",
                table: "couriers",
                newName: "CourierId");

            migrationBuilder.AddForeignKey(
                name: "FK_deliveries_orders_OrderId",
                schema: "public",
                table: "deliveries",
                column: "OrderId",
                principalSchema: "public",
                principalTable: "orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_customers_CustomerId",
                schema: "public",
                table: "orders",
                column: "CustomerId",
                principalSchema: "public",
                principalTable: "customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_RoleId",
                schema: "public",
                table: "users",
                column: "RoleId",
                principalSchema: "public",
                principalTable: "roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
