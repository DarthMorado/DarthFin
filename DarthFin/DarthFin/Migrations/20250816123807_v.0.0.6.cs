using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarthFin.Migrations
{
    /// <inheritdoc />
    public partial class v006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FIN_Entries",
                columns: table => new
                {
                    FIN_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIN_Prices = table.Column<double>(type: "float", nullable: false),
                    FIN_Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIN_Account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIN_Is_Expense = table.Column<bool>(type: "bit", nullable: false),
                    FIN_Entry_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FIN_Real_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FIN_Company = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIN_Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIN_FIL_Id = table.Column<int>(type: "int", nullable: false),
                    FIN_USR_Id = table.Column<int>(type: "int", nullable: false),
                    FIN_External_Id = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIN_Entries", x => x.FIN_ID);
                    table.ForeignKey(
                        name: "FK_FIN_Entries_FIL_Files_FIN_FIL_Id",
                        column: x => x.FIN_FIL_Id,
                        principalTable: "FIL_Files",
                        principalColumn: "FIL_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FIN_Entries_FIN_FIL_Id",
                table: "FIN_Entries",
                column: "FIN_FIL_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FIN_Entries");
        }
    }
}
