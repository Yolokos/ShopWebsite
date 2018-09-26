using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreWebApi.Migrations
{
    public partial class FULLDATABASEFINAL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
               


            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Couriers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "Books",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Orders_OrderId",
                table: "AspNetUsers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Shoppings_ShoppingCartId",
                table: "Books",
                column: "ShoppingCartId",
                principalTable: "Shoppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Couriers_Orders_OrderId",
                table: "Couriers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shoppings_ShoppingCartId",
                table: "Orders",
                column: "ShoppingCartId",
                principalTable: "Shoppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Orders_OrderId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Shoppings_ShoppingCartId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Couriers_Orders_OrderId",
                table: "Couriers");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shoppings_ShoppingCartId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Couriers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "Books",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Orders_OrderId",
                table: "AspNetUsers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Shoppings_ShoppingCartId",
                table: "Books",
                column: "ShoppingCartId",
                principalTable: "Shoppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Couriers_Orders_OrderId",
                table: "Couriers",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shoppings_ShoppingCartId",
                table: "Orders",
                column: "ShoppingCartId",
                principalTable: "Shoppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
