using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LR5.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isLeaf",
                table: "catalogItems",
                newName: "IsLeaf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLeaf",
                table: "catalogItems",
                newName: "isLeaf");
        }
    }
}
