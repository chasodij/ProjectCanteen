using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCanteen.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixcascadedeleteinschooladmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolAdmins_AspNetUsers_UserId",
                table: "SchoolAdmins");

            migrationBuilder.DropIndex(
                name: "IX_SchoolAdmins_UserId",
                table: "SchoolAdmins");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SchoolAdmins",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SchoolAdmins_UserId",
                table: "SchoolAdmins",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolAdmins_AspNetUsers_UserId",
                table: "SchoolAdmins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolAdmins_AspNetUsers_UserId",
                table: "SchoolAdmins");

            migrationBuilder.DropIndex(
                name: "IX_SchoolAdmins_UserId",
                table: "SchoolAdmins");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "SchoolAdmins",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolAdmins_UserId",
                table: "SchoolAdmins",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolAdmins_AspNetUsers_UserId",
                table: "SchoolAdmins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
