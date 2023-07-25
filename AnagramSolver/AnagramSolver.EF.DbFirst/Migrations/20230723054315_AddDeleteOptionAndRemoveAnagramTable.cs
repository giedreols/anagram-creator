using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnagramSolver.EF.DbFirst.Migrations
{
    /// <inheritdoc />
    public partial class AddDeleteOptionAndRemoveAnagramTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Words",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Words");
        }
    }
}
