using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnagramSolver.EF.DbFirst.Migrations
{
    /// <inheritdoc />
    public partial class MakeOrderedFormNotNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderedForm",
                table: "Words",
                type: "nvarchar(50)",
                oldType: "nvarchar(max)",
                nullable: false,
				oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AlterColumn<string>(
				name: "OrderedForm",
				table: "Words",
				type: "nvarchar(max)",
				oldType: "nvarchar(50)",
				nullable: true,
				oldNullable: false);
		}
    }
}
