using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GX.ManageSystem.Portal.Migrations
{
    public partial class Common_PersonnelInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Common_PersonnelInfo",
                columns: table => new
                {
                    PersonnelId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonnelNo = table.Column<int>(nullable: false),
                    PersonnelName = table.Column<string>(maxLength: 50, nullable: true),
                    DepId = table.Column<int>(nullable: false),
                    Avatar = table.Column<string>(maxLength: 200, nullable: true),
                    PersonnelSex = table.Column<int>(maxLength: 1, nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    IdentityCard = table.Column<string>(maxLength: 18, nullable: true),
                    IsWork = table.Column<int>(maxLength: 1, nullable: false),
                    Nation = table.Column<string>(maxLength: 6, nullable: true),
                    MaritalStatus = table.Column<int>(maxLength: 1, nullable: false),
                    LiveAddress = table.Column<string>(maxLength: 200, nullable: true),
                    Phone = table.Column<string>(maxLength: 500, nullable: true),
                    WeChatAccount = table.Column<string>(maxLength: 100, nullable: true),
                    Mailbox = table.Column<string>(maxLength: 500, nullable: true),
                    Degree = table.Column<int>(maxLength: 1, nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    OnBoarDingTime = table.Column<DateTime>(nullable: false),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    TrialTime = table.Column<DateTime>(nullable: false),
                    IsStruggle = table.Column<int>(maxLength: 1, nullable: false),
                    IsSecrecy = table.Column<int>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_PersonnelInfo", x => x.PersonnelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Common_PersonnelInfo");
        }
    }
}
