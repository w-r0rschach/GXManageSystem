using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VMMachineManage.Migrations
{
    public partial class InitialCreate : Migration
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

            migrationBuilder.CreateTable(
                name: "Common_PersonnelInfo",
                columns: table => new
                {
                    PersonnelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassWord = table.Column<string>(maxLength: 16, nullable: true),
                    PersonnelNo = table.Column<int>(nullable: false),
                    PersonnelName = table.Column<string>(maxLength: 50, nullable: true),
                    DepId = table.Column<int>(nullable: false),
                    Avatar = table.Column<string>(maxLength: 200, nullable: true),
                    PersonnelSex = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    IdentityCard = table.Column<string>(maxLength: 18, nullable: true),
                    IsWork = table.Column<int>(nullable: false),
                    Nation = table.Column<string>(maxLength: 6, nullable: true),
                    MaritalStatus = table.Column<int>(nullable: false),
                    LiveAddress = table.Column<string>(maxLength: 200, nullable: true),
                    Phone = table.Column<string>(maxLength: 500, nullable: true),
                    WeChatAccount = table.Column<string>(maxLength: 100, nullable: true),
                    Mailbox = table.Column<string>(maxLength: 500, nullable: true),
                    Degree = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    OnBoarDingTime = table.Column<DateTime>(nullable: false),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    TrialTime = table.Column<DateTime>(nullable: false),
                    IsStruggle = table.Column<int>(nullable: false),
                    IsSecrecy = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_PersonnelInfo", x => x.PersonnelId);
                });

            migrationBuilder.CreateTable(
                name: "MachApplyAndReturn",
                columns: table => new
                {
                    ApplyAndReturnId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OprationType = table.Column<int>(nullable: false),
                    ApplyUserID = table.Column<int>(nullable: false),
                    ExamineUserID = table.Column<int>(nullable: false),
                    MachineInfoID = table.Column<int>(nullable: false),
                    ExamineResult = table.Column<int>(nullable: false),
                    ApplyTime = table.Column<DateTime>(nullable: false),
                    ResultTime = table.Column<DateTime>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachApplyAndReturn", x => x.ApplyAndReturnId);
                });

            migrationBuilder.CreateTable(
                name: "MachineInfo",
                columns: table => new
                {
                    MachineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineIP = table.Column<string>(maxLength: 11, nullable: true),
                    MachineSystem = table.Column<int>(nullable: false),
                    MachineDiskCount = table.Column<double>(nullable: false),
                    MachineMemory = table.Column<double>(nullable: false),
                    MachineState = table.Column<int>(nullable: false),
                    MachineUser = table.Column<string>(maxLength: 20, nullable: true),
                    MachinePassword = table.Column<string>(maxLength: 20, nullable: true),
                    MachApplyAndReturnInfoApplyAndReturnId = table.Column<int>(nullable: true),
                    PersonnelInfoPersonnelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineInfo", x => x.MachineID);
                    table.ForeignKey(
                        name: "FK_MachineInfo_MachApplyAndReturn_MachApplyAndReturnInfoApplyAndReturnId",
                        column: x => x.MachApplyAndReturnInfoApplyAndReturnId,
                        principalTable: "MachApplyAndReturn",
                        principalColumn: "ApplyAndReturnId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachineInfo_Common_PersonnelInfo_PersonnelInfoPersonnelId",
                        column: x => x.PersonnelInfoPersonnelId,
                        principalTable: "Common_PersonnelInfo",
                        principalColumn: "PersonnelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineInfo_MachApplyAndReturnInfoApplyAndReturnId",
                table: "MachineInfo",
                column: "MachApplyAndReturnInfoApplyAndReturnId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineInfo_PersonnelInfoPersonnelId",
                table: "MachineInfo",
                column: "PersonnelInfoPersonnelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Common_Department");

            migrationBuilder.DropTable(
                name: "MachineInfo");

            migrationBuilder.DropTable(
                name: "MachApplyAndReturn");

            migrationBuilder.DropTable(
                name: "Common_PersonnelInfo");
        }
    }
}
