using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrowdfundedArtGallery.Data.Migrations
{
    public partial class fixrelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFolder",
                table: "ArtPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFolder",
                table: "ArtPosts");
        }
    }
}
