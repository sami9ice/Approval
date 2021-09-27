using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class usr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ApprovalGroupName",
                table: "ApprovalLog");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dcb05a3-4924-46b8-b561-0211d7473471",
                column: "ConcurrencyStamp",
                value: "5a32333e-364b-4386-8e3e-ce07d04af11c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dae749f-5550-46db-9d82-409b76a67554",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2feca077-afdc-4e29-8cf5-df0e35364fd2", "cf57de89-fdfd-4545-9372-1f4c67e631cc" });

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "376e968d-ed6b-406e-abcb-f7561f68372d",
                column: "DateCreated",
                value: new DateTime(2021, 9, 17, 23, 26, 12, 926, DateTimeKind.Utc).AddTicks(5392));

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "e03fb776-6a0a-4423-a8b7-24c57570be24",
                column: "DateCreated",
                value: new DateTime(2021, 9, 17, 23, 26, 12, 926, DateTimeKind.Utc).AddTicks(8968));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovalGroupName",
                table: "ApprovalLog",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dcb05a3-4924-46b8-b561-0211d7473471",
                column: "ConcurrencyStamp",
                value: "59701b5d-ce43-4618-aa72-944458287632");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dae749f-5550-46db-9d82-409b76a67554",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "466e280a-2b05-4c41-ad4c-644b0c4b71d4", "dd027a56-126f-4074-9cfc-7ad7414a1ca4" });

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "376e968d-ed6b-406e-abcb-f7561f68372d",
                column: "DateCreated",
                value: new DateTime(2021, 9, 17, 9, 42, 22, 714, DateTimeKind.Utc).AddTicks(552));

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "e03fb776-6a0a-4423-a8b7-24c57570be24",
                column: "DateCreated",
                value: new DateTime(2021, 9, 17, 9, 42, 22, 714, DateTimeKind.Utc).AddTicks(2523));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers",
                column: "GroupId",
                unique: true,
                filter: "[GroupId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_GroupId",
                table: "AspNetUsers",
                column: "GroupId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
