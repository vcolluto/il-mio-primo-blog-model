using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCore_01.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitySportDeleteCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "posts");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "posts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
