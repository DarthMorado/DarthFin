using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarthFin.Migrations
{
    /// <inheritdoc />
    public partial class v003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FIL_Is_Processed",
                table: "FIL_Files",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FIL_Is_Processed",
                table: "FIL_Files");
        }
    }
}
