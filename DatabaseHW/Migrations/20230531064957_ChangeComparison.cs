using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseHW.Migrations
{
    /// <inheritdoc />
    public partial class ChangeComparison : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Job_FreMax",
                table: "Jobs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Job_PeriodMax",
                table: "Jobs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Job_SalaryMax",
                table: "Jobs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_JobCondition_FreMax",
                table: "JobConditions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_JobCondition_PeriodMax",
                table: "JobConditions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_JobCondition_SalaryMax",
                table: "JobConditions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_House_TermMax",
                table: "Houses");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HouseCondition_AreaMax",
                table: "HouseConditions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HouseCondition_PriceMax",
                table: "HouseConditions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HouseCondition_TermMax",
                table: "HouseConditions");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Job_FreMax",
                table: "Jobs",
                sql: "[FreMax] >= [FreMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Job_PeriodMax",
                table: "Jobs",
                sql: "[PeriodMax] >= [PeriodMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Job_SalaryMax",
                table: "Jobs",
                sql: "[SalaryMax] >= [SalaryMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_JobCondition_FreMax",
                table: "JobConditions",
                sql: "[FreMax] >= [FreMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_JobCondition_PeriodMax",
                table: "JobConditions",
                sql: "[PeriodMax] >= [PeriodMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_JobCondition_SalaryMax",
                table: "JobConditions",
                sql: "[SalaryMax] >= [SalaryMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_House_TermMax",
                table: "Houses",
                sql: "[TermMax] >= [TermMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HouseCondition_AreaMax",
                table: "HouseConditions",
                sql: "[AreaMax] >= [AreaMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HouseCondition_PriceMax",
                table: "HouseConditions",
                sql: "[PriceMax] >= [PriceMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HouseCondition_TermMax",
                table: "HouseConditions",
                sql: "[TermMax] >= [TermMin]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Job_FreMax",
                table: "Jobs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Job_PeriodMax",
                table: "Jobs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Job_SalaryMax",
                table: "Jobs");

            migrationBuilder.DropCheckConstraint(
                name: "CK_JobCondition_FreMax",
                table: "JobConditions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_JobCondition_PeriodMax",
                table: "JobConditions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_JobCondition_SalaryMax",
                table: "JobConditions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_House_TermMax",
                table: "Houses");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HouseCondition_AreaMax",
                table: "HouseConditions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HouseCondition_PriceMax",
                table: "HouseConditions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_HouseCondition_TermMax",
                table: "HouseConditions");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Job_FreMax",
                table: "Jobs",
                sql: "[FreMax] > [FreMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Job_PeriodMax",
                table: "Jobs",
                sql: "[PeriodMax] > [PeriodMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Job_SalaryMax",
                table: "Jobs",
                sql: "[SalaryMax] > [SalaryMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_JobCondition_FreMax",
                table: "JobConditions",
                sql: "[FreMax] > [FreMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_JobCondition_PeriodMax",
                table: "JobConditions",
                sql: "[PeriodMax] > [PeriodMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_JobCondition_SalaryMax",
                table: "JobConditions",
                sql: "[SalaryMax] > [SalaryMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_House_TermMax",
                table: "Houses",
                sql: "[TermMax] > [TermMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HouseCondition_AreaMax",
                table: "HouseConditions",
                sql: "[AreaMax] > [AreaMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HouseCondition_PriceMax",
                table: "HouseConditions",
                sql: "[PriceMax] > [PriceMin]");

            migrationBuilder.AddCheckConstraint(
                name: "CK_HouseCondition_TermMax",
                table: "HouseConditions",
                sql: "[TermMax] > [TermMin]");
        }
    }
}
