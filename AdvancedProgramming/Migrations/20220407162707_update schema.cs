using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvancedProgramming.Migrations
{
    public partial class updateschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dep_Name",
                table: "Designation",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Designation",
                newName: "DesignationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Designation",
                newName: "Dep_Name");

            migrationBuilder.RenameColumn(
                name: "DesignationId",
                table: "Designation",
                newName: "Id");
        }
    }
}
