using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarthFin.Migrations
{
    /// <inheritdoc />
    public partial class v002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FIL_Files",
                columns: table => new
                {
                    FIL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIL_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIL_Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FIL_USR_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIL_Files", x => x.FIL_ID);
                    table.ForeignKey(
                        name: "FK_FIL_Files_ADM_Users_FIL_USR_Id",
                        column: x => x.FIL_USR_Id,
                        principalTable: "ADM_Users",
                        principalColumn: "USR_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FIL_Files_FIL_USR_Id",
                table: "FIL_Files",
                column: "FIL_USR_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FIL_Files");
        }
    }
}
