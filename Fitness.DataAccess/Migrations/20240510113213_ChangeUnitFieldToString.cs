using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUnitFieldToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "BodyWeights",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 1,
                column: "Unit",
                value: "kgs");

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 2,
                column: "Unit",
                value: "kgs");

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 3,
                column: "Unit",
                value: "kgs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Unit",
                table: "BodyWeights",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 1,
                column: "Unit",
                value: 0);

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 2,
                column: "Unit",
                value: 0);

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 3,
                column: "Unit",
                value: 0);
        }
    }
}
