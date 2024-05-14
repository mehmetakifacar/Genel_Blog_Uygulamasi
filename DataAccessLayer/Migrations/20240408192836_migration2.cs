using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WriterId",
                table: "Contents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contents_WriterId",
                table: "Contents",
                column: "WriterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Writers_WriterId",
                table: "Contents",
                column: "WriterId",
                principalTable: "Writers",
                principalColumn: "WriterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Writers_WriterId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_WriterId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "WriterId",
                table: "Contents");
        }
    }
}
