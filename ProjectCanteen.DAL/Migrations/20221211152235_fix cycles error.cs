using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCanteen.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixcycleserror : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Canteens_AspNetUsers_TerminalId",
                table: "Canteens");

            migrationBuilder.DropForeignKey(
                name: "FK_Canteens_Schools_SchoolId",
                table: "Canteens");

            migrationBuilder.DropForeignKey(
                name: "FK_CanteenWorkers_AspNetUsers_UserId",
                table: "CanteenWorkers");

            migrationBuilder.DropForeignKey(
                name: "FK_CanteenWorkers_Canteens_CanteenId",
                table: "CanteenWorkers");

            migrationBuilder.DropForeignKey(
                name: "FK_DietaryRestrictions_Students_StudentId",
                table: "DietaryRestrictions");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Canteens_CanteenId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Students_StudentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_AspNetUsers_UserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UserId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_DietaryRestrictions_StudentId",
                table: "DietaryRestrictions");

            migrationBuilder.DropIndex(
                name: "IX_CanteenWorkers_UserId",
                table: "CanteenWorkers");

            migrationBuilder.DropIndex(
                name: "IX_Canteens_TerminalId",
                table: "Canteens");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "DietaryRestrictions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "MenuOfTheDays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CanteenWorkers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DietaryRestrictionStudent",
                columns: table => new
                {
                    DietaryRestrictionsId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaryRestrictionStudent", x => new { x.DietaryRestrictionsId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_DietaryRestrictionStudent_DietaryRestrictions_DietaryRestrictionsId",
                        column: x => x.DietaryRestrictionsId,
                        principalTable: "DietaryRestrictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietaryRestrictionStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuOfTheDays_SchoolId",
                table: "MenuOfTheDays",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_CanteenWorkers_UserId",
                table: "CanteenWorkers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Canteens_TerminalId",
                table: "Canteens",
                column: "TerminalId",
                unique: true,
                filter: "[TerminalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DietaryRestrictionStudent_StudentId",
                table: "DietaryRestrictionStudent",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Canteens_AspNetUsers_TerminalId",
                table: "Canteens",
                column: "TerminalId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Canteens_Schools_SchoolId",
                table: "Canteens",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CanteenWorkers_AspNetUsers_UserId",
                table: "CanteenWorkers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CanteenWorkers_Canteens_CanteenId",
                table: "CanteenWorkers",
                column: "CanteenId",
                principalTable: "Canteens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Canteens_CanteenId",
                table: "Ingredients",
                column: "CanteenId",
                principalTable: "Canteens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuOfTheDays_Schools_SchoolId",
                table: "MenuOfTheDays",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Students_StudentId",
                table: "Orders",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_AspNetUsers_UserId",
                table: "Parents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Canteens_AspNetUsers_TerminalId",
                table: "Canteens");

            migrationBuilder.DropForeignKey(
                name: "FK_Canteens_Schools_SchoolId",
                table: "Canteens");

            migrationBuilder.DropForeignKey(
                name: "FK_CanteenWorkers_AspNetUsers_UserId",
                table: "CanteenWorkers");

            migrationBuilder.DropForeignKey(
                name: "FK_CanteenWorkers_Canteens_CanteenId",
                table: "CanteenWorkers");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Canteens_CanteenId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuOfTheDays_Schools_SchoolId",
                table: "MenuOfTheDays");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Students_StudentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Parents_AspNetUsers_UserId",
                table: "Parents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "DietaryRestrictionStudent");

            migrationBuilder.DropIndex(
                name: "IX_Parents_UserId",
                table: "Parents");

            migrationBuilder.DropIndex(
                name: "IX_MenuOfTheDays_SchoolId",
                table: "MenuOfTheDays");

            migrationBuilder.DropIndex(
                name: "IX_CanteenWorkers_UserId",
                table: "CanteenWorkers");

            migrationBuilder.DropIndex(
                name: "IX_Canteens_TerminalId",
                table: "Canteens");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "MenuOfTheDays");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Parents",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "DietaryRestrictions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CanteenWorkers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserId",
                table: "Parents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DietaryRestrictions_StudentId",
                table: "DietaryRestrictions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CanteenWorkers_UserId",
                table: "CanteenWorkers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Canteens_TerminalId",
                table: "Canteens",
                column: "TerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Canteens_AspNetUsers_TerminalId",
                table: "Canteens",
                column: "TerminalId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Canteens_Schools_SchoolId",
                table: "Canteens",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CanteenWorkers_AspNetUsers_UserId",
                table: "CanteenWorkers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CanteenWorkers_Canteens_CanteenId",
                table: "CanteenWorkers",
                column: "CanteenId",
                principalTable: "Canteens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DietaryRestrictions_Students_StudentId",
                table: "DietaryRestrictions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Canteens_CanteenId",
                table: "Ingredients",
                column: "CanteenId",
                principalTable: "Canteens",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Students_StudentId",
                table: "Orders",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Parents_AspNetUsers_UserId",
                table: "Parents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Classes_ClassId",
                table: "Students",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
