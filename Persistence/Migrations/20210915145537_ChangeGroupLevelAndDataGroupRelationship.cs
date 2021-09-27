using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ChangeGroupLevelAndDataGroupRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ApprovalLevelId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_UserId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "LevelName",
                table: "Data",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "levelId",
                table: "Data",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dcb05a3-4924-46b8-b561-0211d7473471",
                column: "ConcurrencyStamp",
                value: "25289f66-9451-4870-bdd9-381208ba65d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dae749f-5550-46db-9d82-409b76a67554",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "bcbe7266-2f2b-43ee-a5c9-6795db8a57c8", "9bbb1a77-951b-449d-bd02-bea64bfe4d8f" });

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "376e968d-ed6b-406e-abcb-f7561f68372d",
                column: "DateCreated",
                value: new DateTime(2021, 9, 15, 14, 55, 35, 565, DateTimeKind.Utc).AddTicks(4462));

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "e03fb776-6a0a-4423-a8b7-24c57570be24",
                column: "DateCreated",
                value: new DateTime(2021, 9, 15, 14, 55, 35, 565, DateTimeKind.Utc).AddTicks(7688));

            migrationBuilder.CreateIndex(
                name: "IX_Data_levelId",
                table: "Data",
                column: "levelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers",
                column: "GroupId",
                unique: true,
                filter: "[GroupId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ApprovalLevelId",
                table: "AspNetRoles",
                column: "ApprovalLevelId",
                unique: true,
                filter: "[ApprovalLevelId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_GroupId",
                table: "AspNetUsers",
                column: "GroupId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Data_ApprovalLevel_levelId",
                table: "Data",
                column: "levelId",
                principalTable: "ApprovalLevel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Data_ApprovalLevel_levelId",
                table: "Data");

            migrationBuilder.DropIndex(
                name: "IX_Data_levelId",
                table: "Data");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ApprovalLevelId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "LevelName",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "levelId",
                table: "Data");

            migrationBuilder.AlterColumn<string>(
                name: "GroupId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
                value: "c42d2293-cb52-410b-b86d-ec72cf06d848");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dae749f-5550-46db-9d82-409b76a67554",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "2b64bc7f-2fc0-4c21-b1a7-508d9d35f086", "54206a6c-4d3e-4250-bbb8-40dbb9333f63" });

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "376e968d-ed6b-406e-abcb-f7561f68372d",
                column: "DateCreated",
                value: new DateTime(2021, 9, 13, 7, 42, 14, 64, DateTimeKind.Utc).AddTicks(913));

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "e03fb776-6a0a-4423-a8b7-24c57570be24",
                column: "DateCreated",
                value: new DateTime(2021, 9, 13, 7, 42, 14, 64, DateTimeKind.Utc).AddTicks(3247));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ApprovalLevelId",
                table: "AspNetRoles",
                column: "ApprovalLevelId");

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
    }
}
