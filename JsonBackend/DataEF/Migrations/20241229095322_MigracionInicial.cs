using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataEF.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarcasAutos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FIngreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcasAutos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "MarcasAutos",
                columns: new[] { "Id", "Descripcion", "FIngreso" },
                values: new object[,]
                {
                    { 1, "Toyota", new DateTime(2024, 12, 29, 9, 53, 21, 944, DateTimeKind.Utc).AddTicks(6287) },
                    { 2, "Nissan", new DateTime(2024, 12, 29, 9, 53, 21, 944, DateTimeKind.Utc).AddTicks(6693) },
                    { 3, "Hyundai", new DateTime(2024, 12, 29, 9, 53, 21, 944, DateTimeKind.Utc).AddTicks(6695) },
                    { 4, "Suzuki", new DateTime(2024, 12, 29, 9, 53, 21, 944, DateTimeKind.Utc).AddTicks(6696) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarcasAutos");
        }
    }
}
