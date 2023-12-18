using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiConsorcio.Migrations
{
    /// <inheritdoc />
    public partial class AddExport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateExport",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "ExportedBy",
                table: "Leads");

            migrationBuilder.AddColumn<bool>(
                name: "Exported",
                table: "Leads",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Exports",
                columns: table => new
                {
                    ExportId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExportedBy = table.Column<int>(type: "integer", nullable: false),
                    DateExport = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LeadId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exports", x => x.ExportId);
                    table.ForeignKey(
                        name: "FK_Exports_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "LeadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exports_LeadId",
                table: "Exports",
                column: "LeadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exports");

            migrationBuilder.DropColumn(
                name: "Exported",
                table: "Leads");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateExport",
                table: "Leads",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExportedBy",
                table: "Leads",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
