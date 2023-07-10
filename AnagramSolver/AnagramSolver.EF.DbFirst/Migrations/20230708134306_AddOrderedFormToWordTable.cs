using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnagramSolver.EF.DbFirst.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderedFormToWordTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderedForm",
                table: "Words",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderedForm",
                table: "Words");

        }
    }
}
