using Microsoft.EntityFrameworkCore.Migrations;

namespace VMMachineManage.Migrations
{
    public partial class Common_Department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Common_Department",
                columns: table => new
                {
                    DepId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentNumber = table.Column<int>(nullable: false),
                    DepName = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Department", x => x.DepId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Common_Department");
        }
    }
}
