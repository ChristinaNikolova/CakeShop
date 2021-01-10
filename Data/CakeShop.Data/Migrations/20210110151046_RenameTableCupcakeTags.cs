namespace CakeShop.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RenameTableCupcakeTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CupCakeTags_Cupcakes_CupcakeId",
                table: "CupCakeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CupCakeTags_Tags_TagId",
                table: "CupCakeTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CupCakeTags",
                table: "CupCakeTags");

            migrationBuilder.RenameTable(
                name: "CupCakeTags",
                newName: "CupcakeTags");

            migrationBuilder.RenameIndex(
                name: "IX_CupCakeTags_TagId",
                table: "CupcakeTags",
                newName: "IX_CupcakeTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CupcakeTags",
                table: "CupcakeTags",
                columns: new[] { "CupcakeId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeTags_Cupcakes_CupcakeId",
                table: "CupcakeTags",
                column: "CupcakeId",
                principalTable: "Cupcakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeTags_Tags_TagId",
                table: "CupcakeTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeTags_Cupcakes_CupcakeId",
                table: "CupcakeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeTags_Tags_TagId",
                table: "CupcakeTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CupcakeTags",
                table: "CupcakeTags");

            migrationBuilder.RenameTable(
                name: "CupcakeTags",
                newName: "CupCakeTags");

            migrationBuilder.RenameIndex(
                name: "IX_CupcakeTags_TagId",
                table: "CupCakeTags",
                newName: "IX_CupCakeTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CupCakeTags",
                table: "CupCakeTags",
                columns: new[] { "CupcakeId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CupCakeTags_Cupcakes_CupcakeId",
                table: "CupCakeTags",
                column: "CupcakeId",
                principalTable: "Cupcakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CupCakeTags_Tags_TagId",
                table: "CupCakeTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
