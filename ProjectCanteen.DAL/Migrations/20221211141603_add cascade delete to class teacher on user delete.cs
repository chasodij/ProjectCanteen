using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCanteen.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addcascadedeletetoclassteacheronuserdelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeachers_AspNetUsers_UserId",
                table: "ClassTeachers");

            migrationBuilder.DropIndex(
                name: "IX_ClassTeachers_UserId",
                table: "ClassTeachers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ClassTeachers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeachers_UserId",
                table: "ClassTeachers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeachers_AspNetUsers_UserId",
                table: "ClassTeachers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClassTeachers_AspNetUsers_UserId",
                table: "ClassTeachers");

            migrationBuilder.DropIndex(
                name: "IX_ClassTeachers_UserId",
                table: "ClassTeachers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ClassTeachers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_ClassTeachers_UserId",
                table: "ClassTeachers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClassTeachers_AspNetUsers_UserId",
                table: "ClassTeachers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
