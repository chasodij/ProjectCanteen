using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCanteen.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addcanteensettingsfieldsandchangeschoolfieldinmenutocanteen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuOfTheDays_Schools_SchoolId",
                table: "MenuOfTheDays");

            migrationBuilder.RenameColumn(
                name: "SchoolId",
                table: "MenuOfTheDays",
                newName: "CanteenId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuOfTheDays_SchoolId",
                table: "MenuOfTheDays",
                newName: "IX_MenuOfTheDays_CanteenId");

            migrationBuilder.AddColumn<bool>(
                name: "IsCreatedLate",
                table: "MenuOfTheDays",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxStudentDebt",
                table: "Canteens",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinHoursToCreateMenu",
                table: "Canteens",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinHoursToOrder",
                table: "Canteens",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuOfTheDays_Canteens_CanteenId",
                table: "MenuOfTheDays",
                column: "CanteenId",
                principalTable: "Canteens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuOfTheDays_Canteens_CanteenId",
                table: "MenuOfTheDays");

            migrationBuilder.DropColumn(
                name: "IsCreatedLate",
                table: "MenuOfTheDays");

            migrationBuilder.DropColumn(
                name: "MaxStudentDebt",
                table: "Canteens");

            migrationBuilder.DropColumn(
                name: "MinHoursToCreateMenu",
                table: "Canteens");

            migrationBuilder.DropColumn(
                name: "MinHoursToOrder",
                table: "Canteens");

            migrationBuilder.RenameColumn(
                name: "CanteenId",
                table: "MenuOfTheDays",
                newName: "SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuOfTheDays_CanteenId",
                table: "MenuOfTheDays",
                newName: "IX_MenuOfTheDays_SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuOfTheDays_Schools_SchoolId",
                table: "MenuOfTheDays",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
