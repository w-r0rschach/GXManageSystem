using Microsoft.EntityFrameworkCore.Migrations;

namespace VMMachineManage.Migrations
{
    public partial class AddPassWord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassWord",
                table: "Common_PersonnelInfo",
                maxLength: 16,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassWord",
                table: "Common_PersonnelInfo");
        }
    }
}
