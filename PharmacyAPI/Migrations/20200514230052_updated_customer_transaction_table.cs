using Microsoft.EntityFrameworkCore.Migrations;

namespace PharmacyAPI.Migrations
{
    public partial class updated_customer_transaction_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTransactions_Customer_CustomerId",
                table: "CustomerTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "CustomerTransactions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTransactions_Customer_CustomerId",
                table: "CustomerTransactions",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerTransactions_Customer_CustomerId",
                table: "CustomerTransactions");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "CustomerTransactions",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerTransactions_Customer_CustomerId",
                table: "CustomerTransactions",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
