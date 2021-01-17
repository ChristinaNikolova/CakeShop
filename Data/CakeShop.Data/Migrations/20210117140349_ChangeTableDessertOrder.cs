namespace CakeShop.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeTableDessertOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DessertOrders",
                table: "DessertOrders");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "DessertOrders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "DessertOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "DessertOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DessertOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "DessertOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DessertOrders",
                table: "DessertOrders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DessertOrders_DessertId",
                table: "DessertOrders",
                column: "DessertId");

            migrationBuilder.CreateIndex(
                name: "IX_DessertOrders_IsDeleted",
                table: "DessertOrders",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DessertOrders",
                table: "DessertOrders");

            migrationBuilder.DropIndex(
                name: "IX_DessertOrders_DessertId",
                table: "DessertOrders");

            migrationBuilder.DropIndex(
                name: "IX_DessertOrders_IsDeleted",
                table: "DessertOrders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DessertOrders");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "DessertOrders");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "DessertOrders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DessertOrders");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "DessertOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DessertOrders",
                table: "DessertOrders",
                columns: new[] { "DessertId", "OrderId" });
        }
    }
}
