using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tremplin.Migrations
{
    public partial class SharedSheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SharedSheet",
                table: "Patient",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SharedSheet",
                table: "Patient");
        }
    }
}
