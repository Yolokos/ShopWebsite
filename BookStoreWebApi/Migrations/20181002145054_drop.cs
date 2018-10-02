using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreWebApi.Migrations
{
    public partial class drop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_Couriers_Orders_OrderId",
               table: "Couriers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shoppings_ShoppingCartId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Shoppings_ShoppingCartId",
                table: "Books");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_Couriers_Orders_OrderId",
               table: "Couriers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shoppings_ShoppingCartId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
               name: "FK_Books_Shoppings_ShoppingCartId",
               table: "Books");
        }
    }
}
