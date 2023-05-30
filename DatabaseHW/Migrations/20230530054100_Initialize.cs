using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseHW.Migrations
{
    /// <inheritdoc />
    public partial class Initialize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    CommunityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.CommunityId);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Scale = table.Column<byte>(type: "tinyint", nullable: false),
                    Financing = table.Column<byte>(type: "tinyint", nullable: false),
                    Introduction = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                    table.CheckConstraint("CK_Company_Financing", "[Financing] IN (0,1,2,3,4,5,6)");
                    table.CheckConstraint("CK_Company_Scale", "[Scale] IN (0,1,2,3)");
                });

            migrationBuilder.CreateTable(
                name: "HouseConditions",
                columns: table => new
                {
                    HouseConId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PriceMax = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    PriceMin = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    TermMax = table.Column<short>(type: "smallint", nullable: false),
                    TermMin = table.Column<short>(type: "smallint", nullable: false),
                    AreaMax = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    AreaMin = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    Entire = table.Column<byte>(type: "tinyint", nullable: false),
                    Share = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseConditions", x => x.HouseConId);
                    table.CheckConstraint("CK_HouseCondition_AreaMax", "[AreaMax] > [AreaMin]");
                    table.CheckConstraint("CK_HouseCondition_ES", "[Entire] <> 0 OR [Share] <> 0");
                    table.CheckConstraint("CK_HouseCondition_PriceMax", "[PriceMax] > [PriceMin]");
                    table.CheckConstraint("CK_HouseCondition_TermMax", "[TermMax] > [TermMin]");
                });

            migrationBuilder.CreateTable(
                name: "JobConditions",
                columns: table => new
                {
                    JobConId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalaryMax = table.Column<short>(type: "smallint", nullable: false),
                    SalaryMin = table.Column<short>(type: "smallint", nullable: false),
                    PeriodMax = table.Column<byte>(type: "tinyint", nullable: false),
                    PeriodMin = table.Column<byte>(type: "tinyint", nullable: false),
                    FreMax = table.Column<byte>(type: "tinyint", nullable: false),
                    FreMin = table.Column<byte>(type: "tinyint", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    Academic = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobConditions", x => x.JobConId);
                    table.CheckConstraint("CK_JobCondition_Academic", "[Academic] IN (0,1,2,3,4,5)");
                    table.CheckConstraint("CK_JobCondition_FreMax", "[FreMax] > [FreMin]");
                    table.CheckConstraint("CK_JobCondition_PeriodMax", "[PeriodMax] > [PeriodMin]");
                    table.CheckConstraint("CK_JobCondition_SalaryMax", "[SalaryMax] > [SalaryMin]");
                    table.CheckConstraint("CK_JobCondition_Type", "[Type] IN (0,1,2,3,4,5)");
                });

            migrationBuilder.CreateTable(
                name: "Workplaces",
                columns: table => new
                {
                    WorkplaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workplaces", x => x.WorkplaceId);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    HouseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Area = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    TermMax = table.Column<short>(type: "smallint", nullable: false),
                    TermMin = table.Column<short>(type: "smallint", nullable: false),
                    Entire = table.Column<byte>(type: "tinyint", nullable: false),
                    Share = table.Column<byte>(type: "tinyint", nullable: false),
                    DetailUrl = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    CommunityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.HouseId);
                    table.CheckConstraint("CK_House_ES", "[Entire] <> 0 OR [Share] <> 0");
                    table.CheckConstraint("CK_House_TermMax", "[TermMax] > [TermMin]");
                    table.ForeignKey(
                        name: "FK_Houses_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "CommunityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    HouseConId = table.Column<int>(type: "int", nullable: false),
                    JobConId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_HouseConditions_HouseConId",
                        column: x => x.HouseConId,
                        principalTable: "HouseConditions",
                        principalColumn: "HouseConId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_JobConditions_JobConId",
                        column: x => x.JobConId,
                        principalTable: "JobConditions",
                        principalColumn: "JobConId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    SalaryMax = table.Column<short>(type: "smallint", nullable: false),
                    SalaryMin = table.Column<short>(type: "smallint", nullable: false),
                    PeriodMax = table.Column<byte>(type: "tinyint", nullable: false),
                    PeriodMin = table.Column<byte>(type: "tinyint", nullable: false),
                    FreMax = table.Column<byte>(type: "tinyint", nullable: false),
                    FreMin = table.Column<byte>(type: "tinyint", nullable: false),
                    Type = table.Column<short>(type: "smallint", nullable: false),
                    Academic = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: false),
                    DetailUrl = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    WorkplaceId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                    table.CheckConstraint("CK_Job_Academic", "[Academic] IN (0,1,2,3,4,5)");
                    table.CheckConstraint("CK_Job_FreMax", "[FreMax] > [FreMin]");
                    table.CheckConstraint("CK_Job_PeriodMax", "[PeriodMax] > [PeriodMin]");
                    table.CheckConstraint("CK_Job_SalaryMax", "[SalaryMax] > [SalaryMin]");
                    table.CheckConstraint("CK_Job_Type", "[Type] IN (0,1,2,3,4,5)");
                    table.ForeignKey(
                        name: "FK_Jobs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jobs_Workplaces_WorkplaceId",
                        column: x => x.WorkplaceId,
                        principalTable: "Workplaces",
                        principalColumn: "WorkplaceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(10,6)", precision: 10, scale: 6, nullable: false),
                    ApplyTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    Range = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_Records_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_HouseConId",
                table: "Accounts",
                column: "HouseConId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_JobConId",
                table: "Accounts",
                column: "JobConId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Username",
                table: "Accounts",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Communities_Latitude",
                table: "Communities",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_Longitude",
                table: "Communities",
                column: "Longitude");

            migrationBuilder.CreateIndex(
                name: "IX_Houses_CommunityId",
                table: "Houses",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_WorkplaceId",
                table: "Jobs",
                column: "WorkplaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_AccountId",
                table: "Records",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Workplaces_Latitude",
                table: "Workplaces",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Workplaces_Longitude",
                table: "Workplaces",
                column: "Longitude");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Workplaces");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "HouseConditions");

            migrationBuilder.DropTable(
                name: "JobConditions");
        }
    }
}
