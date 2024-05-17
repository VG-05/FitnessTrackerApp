using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddAndSeedMealsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServingSize = table.Column<double>(type: "float", nullable: true),
                    Calories = table.Column<double>(type: "float", nullable: true),
                    Carbohydrates = table.Column<double>(type: "float", nullable: true),
                    Protein = table.Column<double>(type: "float", nullable: true),
                    Fat = table.Column<double>(type: "float", nullable: true),
                    MealTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "BrandName", "Calories", "Carbohydrates", "Date", "Fat", "FoodName", "MealTime", "Protein", "ServingSize" },
                values: new object[] { 1, "", 110.5, 15.6, new DateOnly(2024, 5, 17), 9.0, "Test", "Breakfast", 2.0, 11.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
