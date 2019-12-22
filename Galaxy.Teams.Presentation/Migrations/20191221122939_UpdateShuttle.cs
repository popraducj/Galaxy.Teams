using Microsoft.EntityFrameworkCore.Migrations;

namespace Galaxy.Teams.Presentation.Migrations
{
    public partial class UpdateShuttle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FuelConsumption",
                table: "Shuttles",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "FuelConsumption",
                table: "Shuttles",
                type: "float",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
