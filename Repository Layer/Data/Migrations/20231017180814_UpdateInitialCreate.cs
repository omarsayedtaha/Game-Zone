using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository_Layer.Data.Migrations
{
    public partial class UpdateInitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 3,
                column: "Icon",
                value: "bi bi-pc-display");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Devices",
                keyColumn: "Id",
                keyValue: 3,
                column: "Icon",
                value: "bi bi-pc-displa");
        }
    }
}
