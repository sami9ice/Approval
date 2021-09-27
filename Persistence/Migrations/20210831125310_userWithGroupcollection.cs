using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class userWithGroupcollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_GroupId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers");

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
                columns: new[] { "ConcurrencyStamp", "GroupId" },
                values: new object[] { "7936fdbd-5201-4dbc-9e27-70e511f7a36d", "1db470fa-e184-43b9-9e6c-6fee4dce651e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dae749f-5550-46db-9d82-409b76a67554",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "416ecfa1-a739-46ad-befa-d00b6b789367", "e8e58fff-47ec-441c-939c-5f112fa67e87" });

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "376e968d-ed6b-406e-abcb-f7561f68372d",
                column: "DateCreated",
                value: new DateTime(2021, 8, 31, 12, 53, 8, 500, DateTimeKind.Utc).AddTicks(3123));

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "e03fb776-6a0a-4423-a8b7-24c57570be24",
                column: "DateCreated",
                value: new DateTime(2021, 8, 31, 12, 53, 8, 501, DateTimeKind.Utc).AddTicks(7155));

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
                columns: new[] { "ConcurrencyStamp", "GroupId" },
                values: new object[] { "c0cca326-a33b-4569-8b34-6305b55b4a27", "a6887f44-7b48-4eae-beb6-9fda3bc58c52" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dae749f-5550-46db-9d82-409b76a67554",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "90e579ec-3543-4ffb-b174-e0f2c234e725", "6406a8c8-b451-4294-9111-a7ff71692b1e" });

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "376e968d-ed6b-406e-abcb-f7561f68372d",
                column: "DateCreated",
                value: new DateTime(2021, 8, 30, 13, 45, 44, 343, DateTimeKind.Utc).AddTicks(6761));

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "e03fb776-6a0a-4423-a8b7-24c57570be24",
                column: "DateCreated",
                value: new DateTime(2021, 8, 30, 13, 45, 44, 344, DateTimeKind.Utc).AddTicks(1155));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GroupId",
                table: "AspNetUsers",
                column: "GroupId");

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
