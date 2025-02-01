using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLBH_Core.Moddel.Migrations
{
    /// <inheritdoc />
    public partial class Update_Entity_V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Users");
        }
    }
}
