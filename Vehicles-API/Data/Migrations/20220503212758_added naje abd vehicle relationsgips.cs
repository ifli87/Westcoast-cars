using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vehicles_API.Data.Migrations
{
    public partial class addednajeabdvehiclerelationsgips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Maker",
                table: "Vehicles");

            migrationBuilder.AddColumn<int>(
                name: "MakerId",
                table: "Vehicles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_MakerId",
                table: "Vehicles",
                column: "MakerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Manufacturers_MakerId",
                table: "Vehicles",
                column: "MakerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Manufacturers_MakerId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_MakerId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "MakerId",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "Maker",
                table: "Vehicles",
                type: "TEXT",
                nullable: true);
        }
    }
}
