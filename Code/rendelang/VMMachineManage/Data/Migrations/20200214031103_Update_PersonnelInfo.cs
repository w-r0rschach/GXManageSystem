using Microsoft.EntityFrameworkCore.Migrations;

namespace VMMachineManage.Migrations
{
    public partial class Update_PersonnelInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PassWord",
                table: "Common_PersonnelInfo",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppMaxCount",
                table: "Common_PersonnelInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Common_PersonnelInfo",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppMaxCount",
                table: "Common_PersonnelInfo");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Common_PersonnelInfo");

            migrationBuilder.AlterColumn<string>(
                name: "PassWord",
                table: "Common_PersonnelInfo",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
