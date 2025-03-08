using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTruckApp.Migrations
{
    /// <inheritdoc />
    public partial class AlteredSpecialandOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specials_Orders_Order_ID",
                table: "Specials");

            migrationBuilder.DropIndex(
                name: "IX_Specials_Order_ID",
                table: "Specials");

            migrationBuilder.DropColumn(
                name: "Order_ID",
                table: "Specials");

            migrationBuilder.AddColumn<bool>(
                name: "isApplied",
                table: "Specials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Special_ID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Special_ID",
                table: "Orders",
                column: "Special_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Specials_Special_ID",
                table: "Orders",
                column: "Special_ID",
                principalTable: "Specials",
                principalColumn: "Special_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Specials_Special_ID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Special_ID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "isApplied",
                table: "Specials");

            migrationBuilder.DropColumn(
                name: "Special_ID",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "Order_ID",
                table: "Specials",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Specials_Order_ID",
                table: "Specials",
                column: "Order_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Specials_Orders_Order_ID",
                table: "Specials",
                column: "Order_ID",
                principalTable: "Orders",
                principalColumn: "Order_ID");
        }
    }
}
