using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Fitness.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UserDataAssociatedToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BodyWeights",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Goal",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Meals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Meals",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Meals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Goal",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "BodyWeights",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserID",
                table: "Meals",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Goal_UserID",
                table: "Goal",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BodyWeights_UserID",
                table: "BodyWeights",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BodyWeights_AspNetUsers_UserID",
                table: "BodyWeights",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goal_AspNetUsers_UserID",
                table: "Goal",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_AspNetUsers_UserID",
                table: "Meals",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BodyWeights_AspNetUsers_UserID",
                table: "BodyWeights");

            migrationBuilder.DropForeignKey(
                name: "FK_Goal_AspNetUsers_UserID",
                table: "Goal");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_AspNetUsers_UserID",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_UserID",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Goal_UserID",
                table: "Goal");

            migrationBuilder.DropIndex(
                name: "IX_BodyWeights_UserID",
                table: "BodyWeights");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Goal");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "BodyWeights");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date",
                table: "Meals",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

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
                columns: new[] { "Id", "DailyCalories", "DailyCarbs", "DailyFats", "DailyProtein", "TargetDate", "TargetWeight", "Unit" },
                values: new object[] { 1, 2000, 250, 50, 120, new DateTime(2020, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.0, "kgs" });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Api_Id", "BrandName", "Calories", "Carbohydrates", "Date", "Fat", "FoodName", "MealTime", "Protein", "ServingSizeAmount", "ServingSizeUnit", "Servings" },
                values: new object[] { 1, 534358, "Test Brand inc.", 110.5, 15.6, new DateOnly(2020, 1, 2), 9.0, "Test", "Breakfast", 2.0, 53.0, "g", 1.0 });
        }
    }
}
