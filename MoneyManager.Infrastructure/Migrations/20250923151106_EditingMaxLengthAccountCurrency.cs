using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditingMaxLengthAccountCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Accounts",
                type: "character varying(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(3)",
                oldMaxLength: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Accounts",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4)",
                oldMaxLength: 4);
        }
    }
}
