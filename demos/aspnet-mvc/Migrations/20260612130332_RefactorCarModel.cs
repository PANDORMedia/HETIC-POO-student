using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspnet.Migrations
{
    /// <inheritdoc />
    public partial class RefactorCarModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "DailyPrice",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "NbSeats",
                table: "Cars",
                newName: "CarModelId");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Cars",
                newName: "PlateNumber");

            migrationBuilder.CreateTable(
                name: "CarBrand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBrand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CarBrandId = table.Column<int>(type: "INTEGER", nullable: false),
                    DailyPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    NbSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    Fuel = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarModel_CarBrand_CarBrandId",
                        column: x => x.CarBrandId,
                        principalTable: "CarBrand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarModelId",
                table: "Cars",
                column: "CarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CarModel_CarBrandId",
                table: "CarModel",
                column: "CarBrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarModel_CarModelId",
                table: "Cars",
                column: "CarModelId",
                principalTable: "CarModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarModel_CarModelId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarModel");

            migrationBuilder.DropTable(
                name: "CarBrand");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarModelId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "PlateNumber",
                table: "Cars",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "CarModelId",
                table: "Cars",
                newName: "NbSeats");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Cars",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "DailyPrice",
                table: "Cars",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
