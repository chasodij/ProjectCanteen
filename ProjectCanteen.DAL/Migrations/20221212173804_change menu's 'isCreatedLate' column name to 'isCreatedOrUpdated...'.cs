using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCanteen.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changemenusisCreatedLatecolumnnametoisCreatedOrUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCreatedLate",
                table: "MenuOfTheDays",
                newName: "IsCreatedOrUpdatedLate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCreatedOrUpdatedLate",
                table: "MenuOfTheDays",
                newName: "IsCreatedLate");
        }
    }
}
