namespace CakeShop.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeColumnNameInReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Desserts_CupCakeId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "CupCakeId",
                table: "Reviews",
                newName: "DessertId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_CupCakeId",
                table: "Reviews",
                newName: "IX_Reviews_DessertId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Desserts_DessertId",
                table: "Reviews",
                column: "DessertId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Desserts_DessertId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "DessertId",
                table: "Reviews",
                newName: "CupCakeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_DessertId",
                table: "Reviews",
                newName: "IX_Reviews_CupCakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Desserts_CupCakeId",
                table: "Reviews",
                column: "CupCakeId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
