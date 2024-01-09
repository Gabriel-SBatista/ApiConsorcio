using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiConsorcio.Migrations
{
    /// <inheritdoc />
    public partial class Atualizandoleadseexports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leads_Companys_CompanyId",
                table: "Leads");

            migrationBuilder.DropTable(
                name: "Companys");

            migrationBuilder.DropIndex(
                name: "IX_Leads_CompanyId",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Leads");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Leads",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Leads",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Leads",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Leads");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Leads",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Leads",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(40)",
                oldMaxLength: 40);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Leads",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.CompanyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Leads_CompanyId",
                table: "Leads",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leads_Companys_CompanyId",
                table: "Leads",
                column: "CompanyId",
                principalTable: "Companys",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
