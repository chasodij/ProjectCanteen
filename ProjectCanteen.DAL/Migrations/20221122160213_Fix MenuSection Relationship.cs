using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCanteen.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixMenuSectionRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuSectionId",
                table: "Dishes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_MenuSectionId",
                table: "Dishes",
                column: "MenuSectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_MenuSections_MenuSectionId",
                table: "Dishes",
                column: "MenuSectionId",
                principalTable: "MenuSections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_MenuSections_MenuSectionId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_MenuSectionId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "MenuSectionId",
                table: "Dishes");
        }
    }
}
