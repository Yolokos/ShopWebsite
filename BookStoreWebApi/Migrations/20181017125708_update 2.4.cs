using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreWebApi.Migrations
{
    public partial class update24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
               name: "ShoppingCartId",
               table: "Books",
               nullable: true,
               oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
               name: "ShoppingCartId",
               table: "Orders",
               nullable: true,
               oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
               name: "IX_Orders_ShoppingCartId",
               table: "Orders",
               column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
               name: "IX_Books_ShoppingCartId",
               table: "Books",
               column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
              name: "FK_Books_Shoppings_ShoppingCartId",
              table: "Books",
              column: "ShoppingCartId",
              principalTable: "Shoppings",
              principalColumn: "Id",
              onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
             name: "FK_Orders_Shoppings_ShoppingCartId",
             table: "Orders",
             column: "ShoppingCartId",
             principalTable: "Shoppings",
             principalColumn: "Id",
             onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
