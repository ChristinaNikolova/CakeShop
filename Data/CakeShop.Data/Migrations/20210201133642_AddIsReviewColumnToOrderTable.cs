namespace CakeShop.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddIsReviewColumnToOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReview",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReview",
                table: "Orders");
        }
    }
}
