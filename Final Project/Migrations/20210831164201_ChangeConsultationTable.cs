using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Final_Project.Migrations
{
    public partial class ChangeConsultationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "Minute",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "Second",
                table: "Consultations");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Consultations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Consultations");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Consultations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hour",
                table: "Consultations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Minute",
                table: "Consultations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Second",
                table: "Consultations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
