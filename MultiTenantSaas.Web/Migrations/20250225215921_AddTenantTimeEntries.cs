using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MultiTenantSaas.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantTimeEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hours = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tenant = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntries", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TimeEntries",
                columns: new[] { "Id", "Description", "Hours", "Tenant" },
                values: new object[,]
                {
                    { 1, "2025 Audit", 5.2m, "PeechtreeConsulting" },
                    { 2, "LA City Tax License Submission", 2.5m, "PeechtreeConsulting" },
                    { 3, "Estate Planning Intake Meeting", 4.1m, "GoldmanLaw" },
                    { 4, "Phone call to discuss estate planning", 0.5m, "GoldmanLaw" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeEntries");
        }
    }
}
