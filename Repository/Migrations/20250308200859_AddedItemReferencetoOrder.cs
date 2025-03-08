using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTruckApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedItemReferencetoOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Item_ID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Item_ID1",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Item_ID1",
                table: "Orders",
                column: "Item_ID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Items_Item_ID1",
                table: "Orders",
                column: "Item_ID1",
                principalTable: "Items",
                principalColumn: "Item_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Items_Item_ID1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Item_ID1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Item_ID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Item_ID1",
                table: "Orders");

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
    }
}
