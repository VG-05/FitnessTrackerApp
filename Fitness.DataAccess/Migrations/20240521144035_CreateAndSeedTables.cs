using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateAndSeedTables : Migration
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
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyWeights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetWeight = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Api_Id = table.Column<int>(type: "int", nullable: false),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Servings = table.Column<double>(type: "float", nullable: true),
                    ServingSizeAmount = table.Column<double>(type: "float", nullable: true),
                    ServingSizeUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<double>(type: "float", nullable: true),
                    Carbohydrates = table.Column<double>(type: "float", nullable: true),
                    Protein = table.Column<double>(type: "float", nullable: true),
                    Fat = table.Column<double>(type: "float", nullable: true),
                    MealTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BodyWeights",
                columns: new[] { "Id", "Date", "Unit", "Weight" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kgs", 80.0 },
                    { 2, new DateTime(2020, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "kgs", 75.0 },
                    { 3, new DateTime(2020, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "kgs", 60.0 }
                });

            migrationBuilder.InsertData(
                table: "Goal",
                columns: new[] { "Id", "TargetDate", "TargetWeight", "Unit" },
                values: new object[] { 1, new DateTime(2020, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.0, "kgs" });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Api_Id", "BrandName", "Calories", "Carbohydrates", "Date", "Fat", "FoodName", "MealTime", "Protein", "ServingSizeAmount", "ServingSizeUnit", "Servings" },
                values: new object[] { 1, 534358, "Test Brand inc.", 110.5, 15.6, new DateOnly(2024, 5, 21), 9.0, "Test", "Breakfast", 2.0, 53.0, "g", 1.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyWeights");

            migrationBuilder.DropTable(
                name: "Goal");

            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
