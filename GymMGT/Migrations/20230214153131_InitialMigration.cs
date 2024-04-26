using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GymMGT.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodGroups",
                columns: table => new
                {
                    BloodGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BloodGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodGroups", x => x.BloodGroupID);
                });

            migrationBuilder.CreateTable(
                name: "TrainingLevels",
                columns: table => new
                {
                    TrainingLevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingLevelName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingLevels", x => x.TrainingLevelID);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    TraineeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<string>(type: "varchar(100)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "varchar(50)", nullable: false),
                    Address = table.Column<string>(type: "varchar(50)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    BloodGroupID = table.Column<int>(type: "int", nullable: false),
                    TrainingLevelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.TraineeId);
                    table.ForeignKey(
                        name: "FK_Trainees_BloodGroups_BloodGroupID",
                        column: x => x.BloodGroupID,
                        principalTable: "BloodGroups",
                        principalColumn: "BloodGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainees_TrainingLevels_TrainingLevelID",
                        column: x => x.TrainingLevelID,
                        principalTable: "TrainingLevels",
                        principalColumn: "TrainingLevelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyFeeVouchers",
                columns: table => new
                {
                    MonthlyFeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", rowVersion: true, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TraineeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyFeeVouchers", x => x.MonthlyFeeID);
                    table.ForeignKey(
                        name: "FK_MonthlyFeeVouchers_Trainees_TraineeID",
                        column: x => x.TraineeID,
                        principalTable: "Trainees",
                        principalColumn: "TraineeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyFeeVouchers_TraineeID",
                table: "MonthlyFeeVouchers",
                column: "TraineeID");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_BloodGroupID",
                table: "Trainees",
                column: "BloodGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_TrainingLevelID",
                table: "Trainees",
                column: "TrainingLevelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyFeeVouchers");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "BloodGroups");

            migrationBuilder.DropTable(
                name: "TrainingLevels");
        }
    }
}
