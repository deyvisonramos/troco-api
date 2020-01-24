using Microsoft.EntityFrameworkCore.Migrations;

namespace ChangeApi.Data.Migrations
{
    public partial class InitalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangeHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalChanged = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChangeHistoryItens",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    ChangeHistoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeHistoryItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeHistoryItens_ChangeHistories_ChangeHistoryId",
                        column: x => x.ChangeHistoryId,
                        principalTable: "ChangeHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeHistoryItens_ChangeHistoryId",
                table: "ChangeHistoryItens",
                column: "ChangeHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeHistoryItens");

            migrationBuilder.DropTable(
                name: "ChangeHistories");
        }
    }
}
