using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieCreationAPI.Migrations
{
    /// <inheritdoc />
    public partial class newinitials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "movie",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "movie");
        }
    }
}
