namespace CakeShop.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeCupcakeTableToDessertTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeIngredients_Cupcakes_CupcakeId",
                table: "CupcakeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeLikes_Cupcakes_CupcakeId",
                table: "CupcakeLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeOrders_Cupcakes_CupcakeId",
                table: "CupcakeOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeTags_Cupcakes_CupcakeId",
                table: "CupcakeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Cupcakes_CupCakeId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Cupcakes");

            migrationBuilder.RenameColumn(
                name: "CupcakeId",
                table: "CupcakeTags",
                newName: "DessertId");

            migrationBuilder.RenameColumn(
                name: "CupcakeId",
                table: "CupcakeOrders",
                newName: "DessertId");

            migrationBuilder.RenameColumn(
                name: "CupcakeId",
                table: "CupcakeLikes",
                newName: "DessertId");

            migrationBuilder.RenameColumn(
                name: "CupcakeId",
                table: "CupcakeIngredients",
                newName: "DessertId");

            migrationBuilder.CreateTable(
                name: "Desserts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desserts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Desserts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desserts_CategoryId",
                table: "Desserts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Desserts_IsDeleted",
                table: "Desserts",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeIngredients_Desserts_DessertId",
                table: "CupcakeIngredients",
                column: "DessertId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeLikes_Desserts_DessertId",
                table: "CupcakeLikes",
                column: "DessertId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeOrders_Desserts_DessertId",
                table: "CupcakeOrders",
                column: "DessertId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeTags_Desserts_DessertId",
                table: "CupcakeTags",
                column: "DessertId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Desserts_CupCakeId",
                table: "Reviews",
                column: "CupCakeId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeIngredients_Desserts_DessertId",
                table: "CupcakeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeLikes_Desserts_DessertId",
                table: "CupcakeLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeOrders_Desserts_DessertId",
                table: "CupcakeOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeTags_Desserts_DessertId",
                table: "CupcakeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Desserts_CupCakeId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Desserts");

            migrationBuilder.RenameColumn(
                name: "DessertId",
                table: "CupcakeTags",
                newName: "CupcakeId");

            migrationBuilder.RenameColumn(
                name: "DessertId",
                table: "CupcakeOrders",
                newName: "CupcakeId");

            migrationBuilder.RenameColumn(
                name: "DessertId",
                table: "CupcakeLikes",
                newName: "CupcakeId");

            migrationBuilder.RenameColumn(
                name: "DessertId",
                table: "CupcakeIngredients",
                newName: "CupcakeId");

            migrationBuilder.CreateTable(
                name: "Cupcakes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cupcakes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cupcakes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cupcakes_CategoryId",
                table: "Cupcakes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cupcakes_IsDeleted",
                table: "Cupcakes",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeIngredients_Cupcakes_CupcakeId",
                table: "CupcakeIngredients",
                column: "CupcakeId",
                principalTable: "Cupcakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeLikes_Cupcakes_CupcakeId",
                table: "CupcakeLikes",
                column: "CupcakeId",
                principalTable: "Cupcakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeOrders_Cupcakes_CupcakeId",
                table: "CupcakeOrders",
                column: "CupcakeId",
                principalTable: "Cupcakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeTags_Cupcakes_CupcakeId",
                table: "CupcakeTags",
                column: "CupcakeId",
                principalTable: "Cupcakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Cupcakes_CupCakeId",
                table: "Reviews",
                column: "CupCakeId",
                principalTable: "Cupcakes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
