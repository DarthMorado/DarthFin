using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarthFin.DB.Migrations
{
    /// <inheritdoc />
    public partial class v014 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FIN_Entries_CAT_Categories_FIN_CAT_Id",
                table: "FIN_Entries");

            migrationBuilder.AddForeignKey(
                name: "FK_FIN_Entries_CAT_Categories",
                table: "FIN_Entries",
                column: "FIN_CAT_Id",
                principalTable: "CAT_Categories",
                principalColumn: "CAT_ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FIN_Entries_CAT_Categories",
                table: "FIN_Entries");

            migrationBuilder.AddForeignKey(
                name: "FK_FIN_Entries_CAT_Categories_FIN_CAT_Id",
                table: "FIN_Entries",
                column: "FIN_CAT_Id",
                principalTable: "CAT_Categories",
                principalColumn: "CAT_ID");
        }
    }
}
