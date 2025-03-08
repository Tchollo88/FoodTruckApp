using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodTruckApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSpecialtoBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Specials_Special_ID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Special_ID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Special_ID",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "Special",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Special",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "Special_ID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
