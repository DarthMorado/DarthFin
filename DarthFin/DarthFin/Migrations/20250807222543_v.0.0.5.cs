using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarthFin.Migrations
{
    /// <inheritdoc />
    public partial class v005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CAT_USR_Id",
                table: "CAT_Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Categories_CAT_USR_Id",
                table: "CAT_Categories",
                column: "CAT_USR_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CAT_Categories_ADM_Users_CAT_USR_Id",
                table: "CAT_Categories",
                column: "CAT_USR_Id",
                principalTable: "ADM_Users",
                principalColumn: "USR_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CAT_Categories_ADM_Users_CAT_USR_Id",
                table: "CAT_Categories");

            migrationBuilder.DropIndex(
                name: "IX_CAT_Categories_CAT_USR_Id",
                table: "CAT_Categories");

            migrationBuilder.DropColumn(
                name: "CAT_USR_Id",
                table: "CAT_Categories");
        }
    }
}
