using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DarthFin.DB.Migrations
{
    /// <inheritdoc />
    public partial class v012 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FIN_IS_CAT_Manual",
                table: "FIN_Entries");

            migrationBuilder.AddColumn<int>(
                name: "FIN_FLT_Id",
                table: "FIN_Entries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FLT_FILTER",
                columns: table => new
                {
                    FLT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FLT_Correspondent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FLT_Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FLT_DateFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FLT_DateTill = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FLT_AmountFrom = table.Column<double>(type: "float", nullable: true),
                    FLT_AmountTill = table.Column<double>(type: "float", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLT_FILTER", x => x.FLT_ID);
                    table.ForeignKey(
                        name: "FK_FLT_FILTER_CAT_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CAT_Categories",
                        principalColumn: "CAT_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FLT_FILTER_CategoryId",
                table: "FLT_FILTER",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FLT_FILTER");

            migrationBuilder.DropColumn(
                name: "FIN_FLT_Id",
                table: "FIN_Entries");

            migrationBuilder.AddColumn<bool>(
                name: "FIN_IS_CAT_Manual",
                table: "FIN_Entries",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
