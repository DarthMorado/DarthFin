using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarthFin.DB.Migrations
{
    /// <inheritdoc />
    public partial class v010 : Migration
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

            migrationBuilder.CreateTable(
                name: "CAT_Categories",
                columns: table => new
                {
                    CAT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CAT_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAT_Parent_CAT_Id = table.Column<int>(type: "int", nullable: true),
                    CAT_USR_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAT_Categories", x => x.CAT_ID);
                    table.ForeignKey(
                        name: "FK_CAT_Categories_ADM_Users_CAT_USR_Id",
                        column: x => x.CAT_USR_Id,
                        principalTable: "ADM_Users",
                        principalColumn: "USR_ID");
                    table.ForeignKey(
                        name: "FK_CAT_Categories_CAT_Categories_CAT_Parent_CAT_Id",
                        column: x => x.CAT_Parent_CAT_Id,
                        principalTable: "CAT_Categories",
                        principalColumn: "CAT_ID");
                });

            migrationBuilder.CreateTable(
                name: "FIL_Files",
                columns: table => new
                {
                    FIL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIL_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FIL_Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FIL_USR_Id = table.Column<int>(type: "int", nullable: false),
                    FIL_Is_Processed = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "FIN_Entries",
                columns: table => new
                {
                    FIN_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FIN_Prices = table.Column<double>(type: "float", nullable: true),
                    FIN_Entry_Type = table.Column<int>(type: "int", nullable: false),
                    FIN_Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIN_Account = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIN_Is_Expense = table.Column<bool>(type: "bit", nullable: true),
                    FIN_Entry_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FIN_Real_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FIN_Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIN_Information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIN_FIL_Id = table.Column<int>(type: "int", nullable: false),
                    FIN_USR_Id = table.Column<int>(type: "int", nullable: false),
                    FIN_External_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FIN_Document_Number = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_CAT_Categories_CAT_Parent_CAT_Id",
                table: "CAT_Categories",
                column: "CAT_Parent_CAT_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CAT_Categories_CAT_USR_Id",
                table: "CAT_Categories",
                column: "CAT_USR_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FIL_Files_FIL_USR_Id",
                table: "FIL_Files",
                column: "FIL_USR_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FIN_Entries_FIN_FIL_Id",
                table: "FIN_Entries",
                column: "FIN_FIL_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAT_Categories");

            migrationBuilder.DropTable(
                name: "FIN_Entries");

            migrationBuilder.DropTable(
                name: "FIL_Files");

            migrationBuilder.DropTable(
                name: "ADM_Users");
        }
    }
}
