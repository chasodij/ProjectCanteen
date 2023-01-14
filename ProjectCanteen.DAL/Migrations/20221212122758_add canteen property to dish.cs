using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCanteen.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addcanteenpropertytodish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CanteenId",
                table: "Dishes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_CanteenId",
                table: "Dishes",
                column: "CanteenId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Canteens_CanteenId",
                table: "Dishes",
                column: "CanteenId",
                principalTable: "Canteens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Canteens_CanteenId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_CanteenId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "CanteenId",
                table: "Dishes");
        }
    }
}
