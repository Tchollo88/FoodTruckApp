using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTruckApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Customers_Customer_ID",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Purchases_Purchase_ID",
                table: "Receipts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Specials");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_Customer_ID",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_Purchase_ID",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "Customer_ID",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "Purchase_ID",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Items");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Receipts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
                name: "Date",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "Order_ID",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "Customer_ID",
                table: "Receipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Purchase_ID",
                table: "Receipts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Customer_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Customer_ID);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    MenuItem_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item_ID1 = table.Column<int>(type: "int", nullable: true),
                    Item_ID = table.Column<int>(type: "int", nullable: false),
                    Order_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.MenuItem_ID);
                    table.ForeignKey(
                        name: "FK_MenuItems_Items_Item_ID1",
                        column: x => x.Item_ID1,
                        principalTable: "Items",
                        principalColumn: "Item_ID");
                    table.ForeignKey(
                        name: "FK_MenuItems_Orders_Order_ID",
                        column: x => x.Order_ID,
                        principalTable: "Orders",
                        principalColumn: "Order_ID");
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Purchase_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Purchase_ID);
                });

            migrationBuilder.CreateTable(
                name: "Specials",
                columns: table => new
                {
                    Special_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_ID = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specials", x => x.Special_ID);
                    table.ForeignKey(
                        name: "FK_Specials_Orders_Order_ID",
                        column: x => x.Order_ID,
                        principalTable: "Orders",
                        principalColumn: "Order_ID");
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Ingredient_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItem_ID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Ingredient_ID);
                    table.ForeignKey(
                        name: "FK_Ingredients_MenuItems_MenuItem_ID",
                        column: x => x.MenuItem_ID,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItem_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_Customer_ID",
                table: "Receipts",
                column: "Customer_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_Purchase_ID",
                table: "Receipts",
                column: "Purchase_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_MenuItem_ID",
                table: "Ingredients",
                column: "MenuItem_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_Item_ID1",
                table: "MenuItems",
                column: "Item_ID1");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_Order_ID",
                table: "MenuItems",
                column: "Order_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Specials_Order_ID",
                table: "Specials",
                column: "Order_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Customers_Customer_ID",
                table: "Receipts",
                column: "Customer_ID",
                principalTable: "Customers",
                principalColumn: "Customer_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Purchases_Purchase_ID",
                table: "Receipts",
                column: "Purchase_ID",
                principalTable: "Purchases",
                principalColumn: "Purchase_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
