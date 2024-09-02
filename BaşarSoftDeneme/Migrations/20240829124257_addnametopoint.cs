using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaşarSoftDeneme.Migrations
{
    /// <inheritdoc />
    public partial class addnametopoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Points",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Points");
        }
    }
}
