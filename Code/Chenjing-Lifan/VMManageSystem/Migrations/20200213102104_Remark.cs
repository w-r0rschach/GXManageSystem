using Microsoft.EntityFrameworkCore.Migrations;

namespace VMManageSystem.Migrations
{
    public partial class Remark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "MachApplyAndReturn",
                maxLength: 127,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "MachApplyAndReturn");
        }
    }
}
