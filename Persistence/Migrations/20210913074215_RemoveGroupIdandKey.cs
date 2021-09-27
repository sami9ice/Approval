using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RemoveGroupIdandKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AspNetRoles");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dcb05a3-4924-46b8-b561-0211d7473471",
                columns: new[] { "ConcurrencyStamp", "GroupId" },
                values: new object[] { "c88fa200-03bc-47a8-96ae-0fbc1f4ad8fb", "db47d5f3-7c3f-487b-a5ea-e688cfe28726" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dae749f-5550-46db-9d82-409b76a67554",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "d3f37413-6dd2-47c9-b2a4-43bd6c5c5d7a", "0815d6a5-07ba-4024-a6ed-e1506f0e9313" });

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "376e968d-ed6b-406e-abcb-f7561f68372d",
                column: "DateCreated",
                value: new DateTime(2021, 9, 13, 6, 57, 12, 358, DateTimeKind.Utc).AddTicks(7845));

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "e03fb776-6a0a-4423-a8b7-24c57570be24",
                column: "DateCreated",
                value: new DateTime(2021, 9, 13, 6, 57, 12, 359, DateTimeKind.Utc).AddTicks(2356));
        }
    }
}
