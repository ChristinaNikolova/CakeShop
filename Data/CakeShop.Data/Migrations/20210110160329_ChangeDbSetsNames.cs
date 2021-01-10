namespace CakeShop.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeDbSetsNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeIngredients_Desserts_DessertId",
                table: "CupcakeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeIngredients_Ingredients_IngredientId",
                table: "CupcakeIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeLikes_AspNetUsers_ClientId",
                table: "CupcakeLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeLikes_Desserts_DessertId",
                table: "CupcakeLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeOrders_Desserts_DessertId",
                table: "CupcakeOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeOrders_Orders_OrderId",
                table: "CupcakeOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeTags_Desserts_DessertId",
                table: "CupcakeTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CupcakeTags_Tags_TagId",
                table: "CupcakeTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CupcakeTags",
                table: "CupcakeTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CupcakeOrders",
                table: "CupcakeOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CupcakeLikes",
                table: "CupcakeLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CupcakeIngredients",
                table: "CupcakeIngredients");

            migrationBuilder.RenameTable(
                name: "CupcakeTags",
                newName: "DessertTags");

            migrationBuilder.RenameTable(
                name: "CupcakeOrders",
                newName: "DessertOrders");

            migrationBuilder.RenameTable(
                name: "CupcakeLikes",
                newName: "DessertLikes");

            migrationBuilder.RenameTable(
                name: "CupcakeIngredients",
                newName: "DessertIngredients");

            migrationBuilder.RenameIndex(
                name: "IX_CupcakeTags_TagId",
                table: "DessertTags",
                newName: "IX_DessertTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_CupcakeOrders_OrderId",
                table: "DessertOrders",
                newName: "IX_DessertOrders_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_CupcakeLikes_ClientId",
                table: "DessertLikes",
                newName: "IX_DessertLikes_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_CupcakeIngredients_IngredientId",
                table: "DessertIngredients",
                newName: "IX_DessertIngredients_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DessertTags",
                table: "DessertTags",
                columns: new[] { "DessertId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DessertOrders",
                table: "DessertOrders",
                columns: new[] { "DessertId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DessertLikes",
                table: "DessertLikes",
                columns: new[] { "DessertId", "ClientId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DessertIngredients",
                table: "DessertIngredients",
                columns: new[] { "DessertId", "IngredientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_DessertIngredients_Desserts_DessertId",
                table: "DessertIngredients",
                column: "DessertId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DessertIngredients_Ingredients_IngredientId",
                table: "DessertIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DessertLikes_AspNetUsers_ClientId",
                table: "DessertLikes",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DessertLikes_Desserts_DessertId",
                table: "DessertLikes",
                column: "DessertId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DessertOrders_Desserts_DessertId",
                table: "DessertOrders",
                column: "DessertId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DessertOrders_Orders_OrderId",
                table: "DessertOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DessertTags_Desserts_DessertId",
                table: "DessertTags",
                column: "DessertId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DessertTags_Tags_TagId",
                table: "DessertTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DessertIngredients_Desserts_DessertId",
                table: "DessertIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_DessertIngredients_Ingredients_IngredientId",
                table: "DessertIngredients");

            migrationBuilder.DropForeignKey(
                name: "FK_DessertLikes_AspNetUsers_ClientId",
                table: "DessertLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_DessertLikes_Desserts_DessertId",
                table: "DessertLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_DessertOrders_Desserts_DessertId",
                table: "DessertOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DessertOrders_Orders_OrderId",
                table: "DessertOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_DessertTags_Desserts_DessertId",
                table: "DessertTags");

            migrationBuilder.DropForeignKey(
                name: "FK_DessertTags_Tags_TagId",
                table: "DessertTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DessertTags",
                table: "DessertTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DessertOrders",
                table: "DessertOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DessertLikes",
                table: "DessertLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DessertIngredients",
                table: "DessertIngredients");

            migrationBuilder.RenameTable(
                name: "DessertTags",
                newName: "CupcakeTags");

            migrationBuilder.RenameTable(
                name: "DessertOrders",
                newName: "CupcakeOrders");

            migrationBuilder.RenameTable(
                name: "DessertLikes",
                newName: "CupcakeLikes");

            migrationBuilder.RenameTable(
                name: "DessertIngredients",
                newName: "CupcakeIngredients");

            migrationBuilder.RenameIndex(
                name: "IX_DessertTags_TagId",
                table: "CupcakeTags",
                newName: "IX_CupcakeTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_DessertOrders_OrderId",
                table: "CupcakeOrders",
                newName: "IX_CupcakeOrders_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_DessertLikes_ClientId",
                table: "CupcakeLikes",
                newName: "IX_CupcakeLikes_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_DessertIngredients_IngredientId",
                table: "CupcakeIngredients",
                newName: "IX_CupcakeIngredients_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CupcakeTags",
                table: "CupcakeTags",
                columns: new[] { "DessertId", "TagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CupcakeOrders",
                table: "CupcakeOrders",
                columns: new[] { "DessertId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CupcakeLikes",
                table: "CupcakeLikes",
                columns: new[] { "DessertId", "ClientId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CupcakeIngredients",
                table: "CupcakeIngredients",
                columns: new[] { "DessertId", "IngredientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeIngredients_Desserts_DessertId",
                table: "CupcakeIngredients",
                column: "DessertId",
                principalTable: "Desserts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeIngredients_Ingredients_IngredientId",
                table: "CupcakeIngredients",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CupcakeLikes_AspNetUsers_ClientId",
                table: "CupcakeLikes",
                column: "ClientId",
                principalTable: "AspNetUsers",
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
                name: "FK_CupcakeOrders_Orders_OrderId",
                table: "CupcakeOrders",
                column: "OrderId",
                principalTable: "Orders",
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
                name: "FK_CupcakeTags_Tags_TagId",
                table: "CupcakeTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
