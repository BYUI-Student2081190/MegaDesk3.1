using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MegaDesk3._0.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryStringName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryStringName",
                table: "DeliveryType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryStringName",
                table: "DeliveryType");
        }
    }
}
