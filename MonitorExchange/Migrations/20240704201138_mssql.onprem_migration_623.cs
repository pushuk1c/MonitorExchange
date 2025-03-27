using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonitorExchange.Migrations
{
    /// <inheritdoc />
    public partial class mssqlonprem_migration_623 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileExchanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCreate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StrId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Item = table.Column<int>(type: "int", nullable: false),
                    InAll = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileExchanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goodses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    strId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Artikul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameUkr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameFull = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    View = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryUkr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialUkr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Season = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaSiteUkr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marke = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brend = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataSeason = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goodses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StrId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsSizes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    strId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsSizes_Goodses_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goodses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FEImports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileExchangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GoodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GoodsSizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FEImports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FEImports_FileExchanges_FileExchangeId",
                        column: x => x.FileExchangeId,
                        principalTable: "FileExchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FEImports_GoodsSizes_GoodsSizeId",
                        column: x => x.GoodsSizeId,
                        principalTable: "GoodsSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FEImports_Goodses_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goodses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FEOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileExchangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoodsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GoodsSizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StockId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    discountedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FEOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FEOffers_FileExchanges_FileExchangeId",
                        column: x => x.FileExchangeId,
                        principalTable: "FileExchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FEOffers_GoodsSizes_GoodsSizeId",
                        column: x => x.GoodsSizeId,
                        principalTable: "GoodsSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FEOffers_Goodses_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goodses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FEOffers_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FEImports_FileExchangeId",
                table: "FEImports",
                column: "FileExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_FEImports_GoodsId",
                table: "FEImports",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_FEImports_GoodsSizeId",
                table: "FEImports",
                column: "GoodsSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_FEOffers_FileExchangeId",
                table: "FEOffers",
                column: "FileExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_FEOffers_GoodsId",
                table: "FEOffers",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_FEOffers_GoodsSizeId",
                table: "FEOffers",
                column: "GoodsSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_FEOffers_StockId",
                table: "FEOffers",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsSizes_GoodsId",
                table: "GoodsSizes",
                column: "GoodsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FEImports");

            migrationBuilder.DropTable(
                name: "FEOffers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FileExchanges");

            migrationBuilder.DropTable(
                name: "GoodsSizes");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Goodses");
        }
    }
}
