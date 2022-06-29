using Microsoft.EntityFrameworkCore.Migrations;

namespace Final_Project.Migrations
{
    public partial class ChangeDoctorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SocialMedia_DoctorId",
                table: "SocialMedia");

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Instagram",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Linkedin",
                table: "Doctors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Doctors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_DoctorId",
                table: "SocialMedia",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SocialMedia_DoctorId",
                table: "SocialMedia");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Instagram",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Linkedin",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Doctors");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_DoctorId",
                table: "SocialMedia",
                column: "DoctorId",
                unique: true);
        }
    }
}
