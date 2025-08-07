using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarthFin.Migrations
{
    /// <inheritdoc />
    public partial class v004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAT_Categories",
                columns: table => new
                {
                    CAT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CAT_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAT_Parent_CAT_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Categories", x => x.CAT_ID);
                    table.ForeignKey(
                        name: "FK_CAT_Categories_CAT_Categories_CAT_Parent_CAT_Id",
                        column: x => x.CAT_Parent_CAT_Id,
                        principalTable: "CAT_Categories",
                        principalColumn: "CAT_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Categories_CAT_Parent_CAT_Id",
                table: "CAT_Categories",
                column: "CAT_Parent_CAT_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Categories");
        }
    }
}
