using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreWebApi.Migrations
{
    public partial class newordersbooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Shoppings_ShoppingCartId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Shoppings_ShoppingCartId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Shoppings");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShoppingCartId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Books_ShoppingCartId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "OrderBook",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    BookId = table.Column<string>(nullable: false),
                    CountCopy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderBook", x => new { x.OrderId, x.BookId });
                    table.ForeignKey(
                        name: "FK_OrderBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "ISBN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderBook_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderBook_BookId",
                table: "OrderBook",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderBook");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Shoppings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountCopy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoppings", x => x.Id);
                });

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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Shoppings_ShoppingCartId",
                table: "Orders",
                column: "ShoppingCartId",
                principalTable: "Shoppings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
