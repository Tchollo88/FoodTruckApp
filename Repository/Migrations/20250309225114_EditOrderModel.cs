using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTruckApp.Migrations
{
    /// <inheritdoc />
    public partial class EditOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Orders_Item_ID",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Items_Item_ID",
                table: "Orders",
                column: "Item_ID",
                principalTable: "Items",
                principalColumn: "Item_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Items_Item_ID",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Orders_Item_ID",
                table: "Orders",
                column: "Item_ID",
                principalTable: "Orders",
                principalColumn: "Order_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
