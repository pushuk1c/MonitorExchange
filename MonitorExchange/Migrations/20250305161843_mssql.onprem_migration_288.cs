using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitorExchange.Migrations
{
    /// <inheritdoc />
    public partial class mssqlonprem_migration_288 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Goodses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Goodses");
        }
    }
}
