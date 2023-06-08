using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaDesk3._0.Migrations
{
    /// <inheritdoc />
    public partial class TotalCost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceBetween1000And2000",
                table: "DeliveryType");

            migrationBuilder.DropColumn(
                name: "PriceOver2000",
                table: "DeliveryType");

            migrationBuilder.RenameColumn(
                name: "PriceUnder1000",
                table: "DeliveryType",
                newName: "DeliveryPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "DeskQuote",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryName",
                table: "DeliveryType",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "DeskQuote");

            migrationBuilder.RenameColumn(
                name: "DeliveryPrice",
                table: "DeliveryType",
                newName: "PriceUnder1000");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryName",
                table: "DeliveryType",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceBetween1000And2000",
                table: "DeliveryType",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceOver2000",
                table: "DeliveryType",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
