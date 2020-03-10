using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ClaimPropUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegionCode",
                table: "Claims");

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Claims",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Claims");

            migrationBuilder.AddColumn<string>(
                name: "RegionCode",
                table: "Claims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
