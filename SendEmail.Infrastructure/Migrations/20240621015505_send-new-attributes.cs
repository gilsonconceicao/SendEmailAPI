using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SendEmail.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sendnewattributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailAddres",
                table: "SendEmails",
                newName: "ToEmailAddress");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "SendEmails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FromEmailAddress",
                table: "SendEmails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "SendEmails",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "SendEmails");

            migrationBuilder.DropColumn(
                name: "FromEmailAddress",
                table: "SendEmails");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "SendEmails");

            migrationBuilder.RenameColumn(
                name: "ToEmailAddress",
                table: "SendEmails",
                newName: "EmailAddres");
        }
    }
}
