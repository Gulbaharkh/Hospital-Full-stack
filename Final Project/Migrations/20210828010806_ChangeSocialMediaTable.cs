using Microsoft.EntityFrameworkCore.Migrations;

namespace Final_Project.Migrations
{
    public partial class ChangeSocialMediaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "SocialMedia",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SocialMedia_DoctorId",
                table: "SocialMedia",
                column: "DoctorId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SocialMedia_Doctors_DoctorId",
                table: "SocialMedia",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialMedia_Doctors_DoctorId",
                table: "SocialMedia");

            migrationBuilder.DropIndex(
                name: "IX_SocialMedia_DoctorId",
                table: "SocialMedia");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "SocialMedia");
        }
    }
}
