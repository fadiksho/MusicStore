using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStore.MVC.Migrations
{
  public partial class Initial_Create : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Albums",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(nullable: true),
            Description = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Albums", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Genres",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Genres", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Songs",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            Name = table.Column<string>(nullable: true),
            AlbumId = table.Column<int>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Songs", x => x.Id);
            table.ForeignKey(
                      name: "FK_Songs_Albums_AlbumId",
                      column: x => x.AlbumId,
                      principalTable: "Albums",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.SetNull);
          });

      migrationBuilder.CreateTable(
          name: "GenreSongEntity",
          columns: table => new
          {
            GenreId = table.Column<int>(nullable: false),
            SongId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_GenreSongEntity", x => new { x.GenreId, x.SongId });
            table.ForeignKey(
                      name: "FK_GenreSongEntity_Genres_GenreId",
                      column: x => x.GenreId,
                      principalTable: "Genres",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_GenreSongEntity_Songs_SongId",
                      column: x => x.SongId,
                      principalTable: "Songs",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_GenreSongEntity_SongId",
          table: "GenreSongEntity",
          column: "SongId");

      migrationBuilder.CreateIndex(
          name: "IX_Songs_AlbumId",
          table: "Songs",
          column: "AlbumId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "GenreSongEntity");

      migrationBuilder.DropTable(
          name: "Genres");

      migrationBuilder.DropTable(
          name: "Songs");

      migrationBuilder.DropTable(
          name: "Albums");
    }
  }
}
