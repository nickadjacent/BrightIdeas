using Microsoft.EntityFrameworkCore.Migrations;

namespace BrightIdeas.Migrations
{
    public partial class Like : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUpLike",
                table: "Likes",
                newName: "IsLiked");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLiked",
                table: "Likes",
                newName: "IsUpLike");
        }
    }
}
