using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd_App.Migrations
{
    public partial class createdNewEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestEntity");

            migrationBuilder.EnsureSchema(
                name: "filter");

            migrationBuilder.EnsureSchema(
                name: "data");

            migrationBuilder.EnsureSchema(
                name: "deatails");

            migrationBuilder.CreateTable(
                name: "albumCategorys",
                schema: "filter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albumCategorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "albums",
                schema: "filter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contactRequests",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contactRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "files",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "infos",
                schema: "deatails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_infos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                schema: "deatails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    Field = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlbumAlbumCategory",
                schema: "filter",
                columns: table => new
                {
                    AlbumsId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumAlbumCategory", x => new { x.AlbumsId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_AlbumAlbumCategory_albumCategorys_CategoriesId",
                        column: x => x.CategoriesId,
                        principalSchema: "filter",
                        principalTable: "albumCategorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumAlbumCategory_albums_AlbumsId",
                        column: x => x.AlbumsId,
                        principalSchema: "filter",
                        principalTable: "albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumAlbumCategory_CategoriesId",
                schema: "filter",
                table: "AlbumAlbumCategory",
                column: "CategoriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumAlbumCategory",
                schema: "filter");

            migrationBuilder.DropTable(
                name: "contactRequests",
                schema: "data");

            migrationBuilder.DropTable(
                name: "files",
                schema: "data");

            migrationBuilder.DropTable(
                name: "infos",
                schema: "deatails");

            migrationBuilder.DropTable(
                name: "services",
                schema: "deatails");

            migrationBuilder.DropTable(
                name: "albumCategorys",
                schema: "filter");

            migrationBuilder.DropTable(
                name: "albums",
                schema: "filter");

            migrationBuilder.CreateTable(
                name: "TestEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "VARCHAR(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEntity", x => x.Id);
                });
        }
    }
}
