using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreWebApi.Migrations
{
    public partial class shop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
