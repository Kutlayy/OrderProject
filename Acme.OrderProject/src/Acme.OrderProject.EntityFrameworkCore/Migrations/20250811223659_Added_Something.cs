using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.OrderProject.Migrations
{
    /// <inheritdoc />
    public partial class Added_Something : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RiskLimit",
                table: "AppCustomers",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,21)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RiskLimit",
                table: "AppCustomers",
                type: "numeric(18,21)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");
        }
    }
}
