using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VMManageSystem.Migrations
{
    public partial class VirtualMachineModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Common_PersonnelInfo",
                columns: table => new
                {
                    PersonnelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonnelNo = table.Column<int>(nullable: false),
                    PersonnelName = table.Column<string>(nullable: true),
                    DepId = table.Column<byte>(nullable: false),
                    Avatar = table.Column<string>(nullable: true),
                    PersonnelSex = table.Column<byte>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    IdentityCard = table.Column<string>(nullable: true),
                    IsWork = table.Column<byte>(nullable: false),
                    Nation = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<byte>(nullable: false),
                    LiveAddress = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    WeChatAccount = table.Column<string>(nullable: true),
                    Mailbox = table.Column<string>(nullable: true),
                    Degree = table.Column<byte>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    OnboardingTime = table.Column<DateTime>(nullable: false),
                    DepartureTime = table.Column<DateTime>(nullable: false),
                    TrialTime = table.Column<DateTime>(nullable: false),
                    IsStruggle = table.Column<byte>(nullable: false),
                    IsSecrecy = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_PersonnelInfo", x => x.PersonnelId);
                });

            migrationBuilder.CreateTable(
                name: "MachineInfo",
                columns: table => new
                {
                    MachineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineIP = table.Column<string>(maxLength: 11, nullable: true),
                    MachineSystem = table.Column<byte>(nullable: false),
                    MachineDiskCount = table.Column<double>(nullable: false),
                    MachineMemory = table.Column<double>(nullable: false),
                    MachineState = table.Column<byte>(nullable: false),
                    MachineUser = table.Column<string>(nullable: true),
                    MachinePassword = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineInfo", x => x.MachineId);
                });

            migrationBuilder.CreateTable(
                name: "MachApplyAndReturn",
                columns: table => new
                {
                    ApplyAndReturnId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OprationType = table.Column<byte>(nullable: false),
                    ApplyUserID = table.Column<int>(nullable: false),
                    ExamineUserID = table.Column<int>(nullable: false),
                    MachineInfoID = table.Column<int>(nullable: false),
                    ExamineResult = table.Column<byte>(nullable: false),
                    ApplyTime = table.Column<DateTime>(nullable: false),
                    ResultTime = table.Column<DateTime>(nullable: false),
                    PersonnelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachApplyAndReturn", x => x.ApplyAndReturnId);
                    table.ForeignKey(
                        name: "FK_MachApplyAndReturn_MachineInfo_MachineInfoID",
                        column: x => x.MachineInfoID,
                        principalTable: "MachineInfo",
                        principalColumn: "MachineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachApplyAndReturn_Common_PersonnelInfo_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Common_PersonnelInfo",
                        principalColumn: "PersonnelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachApplyAndReturn_MachineInfoID",
                table: "MachApplyAndReturn",
                column: "MachineInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_MachApplyAndReturn_PersonnelId",
                table: "MachApplyAndReturn",
                column: "PersonnelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachApplyAndReturn");

            migrationBuilder.DropTable(
                name: "MachineInfo");

            migrationBuilder.DropTable(
                name: "Common_PersonnelInfo");
        }
    }
}
