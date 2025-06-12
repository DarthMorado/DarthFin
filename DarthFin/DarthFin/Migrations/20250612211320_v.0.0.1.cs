using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarthFin.Migrations
{
    /// <inheritdoc />
    public partial class v001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ADM_Users",
                columns: table => new
                {
                    USR_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USR_Gmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USR_Is_Admin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ADM_Users", x => x.USR_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ADM_Users");
        }
    }
}
