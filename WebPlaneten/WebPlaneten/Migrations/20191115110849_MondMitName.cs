using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPlaneten.Migrations
{
    public partial class MondMitName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Durchmesser",
                table: "Mond",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Mond",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Durchmesser",
                table: "Mond");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Mond");
        }
    }
}
