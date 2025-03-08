using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTruckApp.Migrations
{
    /// <inheritdoc />
    public partial class AlteredOrdertoLookforItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Orders_Order_ID",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_Order_ID",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "Order_ID",
                table: "MenuItems");

            migrationBuilder.AddColumn<int>(
                name: "Order_ID",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_Order_ID",
                table: "Items",
                column: "Order_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Orders_Order_ID",
                table: "Items",
                column: "Order_ID",
                principalTable: "Orders",
                principalColumn: "Order_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Orders_Order_ID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_Order_ID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Order_ID",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "Order_ID",
                table: "MenuItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_Order_ID",
                table: "MenuItems",
                column: "Order_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Orders_Order_ID",
                table: "MenuItems",
                column: "Order_ID",
                principalTable: "Orders",
                principalColumn: "Order_ID");
        }
    }
}
