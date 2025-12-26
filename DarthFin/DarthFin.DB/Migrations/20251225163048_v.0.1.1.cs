using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarthFin.DB.Migrations
{
    /// <inheritdoc />
    public partial class v011 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FIN_CAT_Id",
                table: "FIN_Entries",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FIN_IS_CAT_Manual",
                table: "FIN_Entries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_FIN_Entries_FIN_CAT_Id",
                table: "FIN_Entries",
                column: "FIN_CAT_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FIN_Entries_CAT_Categories_FIN_CAT_Id",
                table: "FIN_Entries",
                column: "FIN_CAT_Id",
                principalTable: "CAT_Categories",
                principalColumn: "CAT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FIN_Entries_CAT_Categories_FIN_CAT_Id",
                table: "FIN_Entries");

            migrationBuilder.DropIndex(
                name: "IX_FIN_Entries_FIN_CAT_Id",
                table: "FIN_Entries");

            migrationBuilder.DropColumn(
                name: "FIN_CAT_Id",
                table: "FIN_Entries");

            migrationBuilder.DropColumn(
                name: "FIN_IS_CAT_Manual",
                table: "FIN_Entries");
        }
    }
}
