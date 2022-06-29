using Microsoft.EntityFrameworkCore.Migrations;

namespace Final_Project.Migrations
{
    public partial class ChangeCounterTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Customer",
                table: "Counters");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Counters");

            migrationBuilder.DropColumn(
                name: "Doctor",
                table: "Counters");

            migrationBuilder.DropColumn(
                name: "Hospital",
                table: "Counters");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Counters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Counters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "Counters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Counters");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Counters");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "Counters");

            migrationBuilder.AddColumn<int>(
                name: "Customer",
                table: "Counters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Department",
                table: "Counters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Doctor",
                table: "Counters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hospital",
                table: "Counters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
