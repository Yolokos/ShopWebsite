using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreWebApi.Migrations
{
    public partial class book : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Shoppings_ShoppingCartId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "Books",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Shoppings_ShoppingCartId",
                table: "Books",
                column: "ShoppingCartId",
                principalTable: "Shoppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Shoppings_ShoppingCartId",
                table: "Books");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartId",
                table: "Books",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Shoppings_ShoppingCartId",
                table: "Books",
                column: "ShoppingCartId",
                principalTable: "Shoppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
