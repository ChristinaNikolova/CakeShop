namespace CakeShop.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangePreparationTimeAndCookingTimeDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CookingTime",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreparationTime",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CookingTime",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "PreparationTime",
                table: "Recipes");
        }
    }
}
