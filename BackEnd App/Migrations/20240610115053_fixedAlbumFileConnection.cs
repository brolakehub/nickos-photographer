using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_App.Migrations
{
    public partial class fixedAlbumFileConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Field",
                schema: "deatails",
                table: "services");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                schema: "deatails",
                table: "services",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AlbumFile",
                columns: table => new
                {
                    AlbumsId = table.Column<int>(type: "int", nullable: false),
                    FilesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumFile", x => new { x.AlbumsId, x.FilesId });
                    table.ForeignKey(
                        name: "FK_AlbumFile_albums_AlbumsId",
                        column: x => x.AlbumsId,
                        principalSchema: "filter",
                        principalTable: "albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumFile_files_FilesId",
                        column: x => x.FilesId,
                        principalSchema: "data",
                        principalTable: "files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_services_FileId",
                schema: "deatails",
                table: "services",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumFile_FilesId",
                table: "AlbumFile",
                column: "FilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_services_files_FileId",
                schema: "deatails",
                table: "services",
                column: "FileId",
                principalSchema: "data",
                principalTable: "files",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_services_files_FileId",
                schema: "deatails",
                table: "services");

            migrationBuilder.DropTable(
                name: "AlbumFile");

            migrationBuilder.DropIndex(
                name: "IX_services_FileId",
                schema: "deatails",
                table: "services");

            migrationBuilder.DropColumn(
                name: "FileId",
                schema: "deatails",
                table: "services");

            migrationBuilder.AddColumn<string>(
                name: "Field",
                schema: "deatails",
                table: "services",
                type: "nvarchar(MAX)",
                nullable: false,
                defaultValue: "");
        }
    }
}
