using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_App.Migrations
{
    public partial class tableNameTypo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumAlbumCategory_albumCategorys_CategoriesId",
                schema: "filter",
                table: "AlbumAlbumCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_albumCategorys",
                schema: "filter",
                table: "albumCategorys");

            migrationBuilder.RenameTable(
                name: "albumCategorys",
                schema: "filter",
                newName: "albumCategories",
                newSchema: "filter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_albumCategories",
                schema: "filter",
                table: "albumCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumAlbumCategory_albumCategories_CategoriesId",
                schema: "filter",
                table: "AlbumAlbumCategory",
                column: "CategoriesId",
                principalSchema: "filter",
                principalTable: "albumCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumAlbumCategory_albumCategories_CategoriesId",
                schema: "filter",
                table: "AlbumAlbumCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_albumCategories",
                schema: "filter",
                table: "albumCategories");

            migrationBuilder.RenameTable(
                name: "albumCategories",
                schema: "filter",
                newName: "albumCategorys",
                newSchema: "filter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_albumCategorys",
                schema: "filter",
                table: "albumCategorys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumAlbumCategory_albumCategorys_CategoriesId",
                schema: "filter",
                table: "AlbumAlbumCategory",
                column: "CategoriesId",
                principalSchema: "filter",
                principalTable: "albumCategorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
