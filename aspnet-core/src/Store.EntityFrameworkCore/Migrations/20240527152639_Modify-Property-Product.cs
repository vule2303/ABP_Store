using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Migrations
{
    public partial class ModifyPropertyProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Visiblity",
                table: "AppProducts",
                newName: "Visibility");

            migrationBuilder.AddColumn<string>(
                name: "CreategoryName",
                table: "AppProducts",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreategorySlug",
                table: "AppProducts",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreategoryName",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "CreategorySlug",
                table: "AppProducts");

            migrationBuilder.RenameColumn(
                name: "Visibility",
                table: "AppProducts",
                newName: "Visiblity");
        }
    }
}
