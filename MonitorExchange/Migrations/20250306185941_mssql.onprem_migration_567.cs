using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitorExchange.Migrations
{
    /// <inheritdoc />
    public partial class mssqlonprem_migration_567 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUpload",
                table: "FileExchanges",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUpload",
                table: "FileExchanges");
        }
    }
}
