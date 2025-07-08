using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiProject.Migrations
{
    /// <inheritdoc />
    public partial class UniqueHouseholdNameIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Households_Name_OwnerId",
                table: "Households",
                columns: new[] { "Name", "OwnerId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Households_Name_OwnerId",
                table: "Households");
        }
    }
}
