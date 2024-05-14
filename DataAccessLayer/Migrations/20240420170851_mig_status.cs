using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig_status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Writers",
                newName: "WriterStatus");

            migrationBuilder.AddColumn<bool>(
                name: "HeadingStatus",
                table: "Headings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadingStatus",
                table: "Headings");

            migrationBuilder.RenameColumn(
                name: "WriterStatus",
                table: "Writers",
                newName: "Status");
        }
    }
}
