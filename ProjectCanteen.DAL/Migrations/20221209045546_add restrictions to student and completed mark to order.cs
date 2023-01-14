using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCanteen.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addrestrictionstostudentandcompletedmarktoorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "DietaryRestrictions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DietaryRestrictions_StudentId",
                table: "DietaryRestrictions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_DietaryRestrictions_Students_StudentId",
                table: "DietaryRestrictions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietaryRestrictions_Students_StudentId",
                table: "DietaryRestrictions");

            migrationBuilder.DropIndex(
                name: "IX_DietaryRestrictions_StudentId",
                table: "DietaryRestrictions");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "DietaryRestrictions");
        }
    }
}
