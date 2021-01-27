using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialApp.Migrations
{
    public partial class updateddynamictable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "DynamicTable");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "DynamicTable");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "DynamicTable");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "DynamicTable");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "DynamicTable");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "DynamicTable",
                newName: "EntityType");

            migrationBuilder.RenameColumn(
                name: "SSN",
                table: "DynamicTable",
                newName: "EntityName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EntityType",
                table: "DynamicTable",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "EntityName",
                table: "DynamicTable",
                newName: "SSN");

            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                table: "DynamicTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "DynamicTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "DynamicTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "DynamicTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "DynamicTable",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
