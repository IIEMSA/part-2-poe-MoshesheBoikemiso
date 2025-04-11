using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLDV6211_WebApplication.Migrations
{
    /// <inheritdoc />
    public partial class RenameVenueLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VenueLocation",
                table: "Venues",
                newName: "Location");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Venues",
                newName: "VenueLocation");
        }
    }
}
