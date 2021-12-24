using Microsoft.EntityFrameworkCore.Migrations;

namespace PharmacyAPI.Migrations
{
    public partial class Updated_customerId_property_and_added_customers_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTransactions_RegisteredCustomers_CustomerId",
                table: "CustomerTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_RegisteredCustomers_Shops_ShopId",
                table: "RegisteredCustomers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegisteredCustomers",
                table: "RegisteredCustomers");

            migrationBuilder.RenameTable(
                name: "RegisteredCustomers",
                newName: "Customer");

            migrationBuilder.RenameIndex(
                name: "IX_RegisteredCustomers_ShopId",
                table: "Customer",
                newName: "IX_Customer_ShopId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Shops_ShopId",
                table: "Customer",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTransactions_Customer_CustomerId",
                table: "CustomerTransactions",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Shops_ShopId",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTransactions_Customer_CustomerId",
                table: "CustomerTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "RegisteredCustomers");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_ShopId",
                table: "RegisteredCustomers",
                newName: "IX_RegisteredCustomers_ShopId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegisteredCustomers",
                table: "RegisteredCustomers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTransactions_RegisteredCustomers_CustomerId",
                table: "CustomerTransactions",
                column: "CustomerId",
                principalTable: "RegisteredCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegisteredCustomers_Shops_ShopId",
                table: "RegisteredCustomers",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
