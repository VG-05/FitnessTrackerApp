using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWeightAndTargetWeightToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TargetWeight",
                table: "Goal",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "BodyWeights",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 1,
                column: "Weight",
                value: 80.0);

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 2,
                column: "Weight",
                value: 75.0);

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 3,
                column: "Weight",
                value: 60.0);

            migrationBuilder.UpdateData(
                table: "Goal",
                keyColumn: "Id",
                keyValue: 1,
                column: "TargetWeight",
                value: 50.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TargetWeight",
                table: "Goal",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Weight",
                table: "BodyWeights",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 1,
                column: "Weight",
                value: 80);

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 2,
                column: "Weight",
                value: 75);

            migrationBuilder.UpdateData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 3,
                column: "Weight",
                value: 60);

            migrationBuilder.UpdateData(
                table: "Goal",
                keyColumn: "Id",
                keyValue: 1,
                column: "TargetWeight",
                value: 50);
        }
    }
}
