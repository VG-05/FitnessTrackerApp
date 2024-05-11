using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAndSeedBodyWeightTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyWeights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyWeights", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BodyWeights",
                columns: new[] { "Id", "Date", "Unit", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 80 },
                    { 2, new DateTime(2020, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 75 },
                    { 3, new DateTime(2020, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 60 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyWeights");
        }
    }
}
