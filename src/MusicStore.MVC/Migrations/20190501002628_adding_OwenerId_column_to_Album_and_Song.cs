using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicStore.MVC.Migrations
{
  public partial class adding_OwenerId_column_to_Album_and_Song : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "OwenerId",
          table: "Songs",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "OwenerId",
          table: "Albums",
          nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "OwenerId",
          table: "Songs");

      migrationBuilder.DropColumn(
          name: "OwenerId",
          table: "Albums");
    }
  }
}
