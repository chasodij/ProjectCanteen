﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectCanteen.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixcycleserror1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Parents_ParentsId",
                table: "ParentStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Students_ChildrenId",
                table: "ParentStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserId",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Parents_ParentsId",
                table: "ParentStudent",
                column: "ParentsId",
                principalTable: "Parents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Students_ChildrenId",
                table: "ParentStudent",
                column: "ChildrenId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Parents_ParentsId",
                table: "ParentStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_ParentStudent_Students_ChildrenId",
                table: "ParentStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_UserId",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Students",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Parents_ParentsId",
                table: "ParentStudent",
                column: "ParentsId",
                principalTable: "Parents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParentStudent_Students_ChildrenId",
                table: "ParentStudent",
                column: "ChildrenId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_AspNetUsers_UserId",
                table: "Students",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
