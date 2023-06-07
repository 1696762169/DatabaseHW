using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseHW.Migrations
{
    /// <inheritdoc />
    public partial class SetRecordRangeToFloat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Range",
                table: "Records",
                type: "decimal(6,2)",
                precision: 6,
                scale: 2,
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Range",
                table: "Records",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(6,2)",
                oldPrecision: 6,
                oldScale: 2);
        }
    }
}
