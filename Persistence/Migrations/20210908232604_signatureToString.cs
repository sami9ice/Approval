using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class signatureToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Signature",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dcb05a3-4924-46b8-b561-0211d7473471",
                columns: new[] { "ConcurrencyStamp", "GroupId" },
                values: new object[] { "95c021ef-74b1-464b-94f9-2f4c53081acb", "468b4f60-c4c1-4c96-985a-619c863a95b9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dae749f-5550-46db-9d82-409b76a67554",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "Signature" },
                values: new object[] { "1d5dde15-447a-4611-a33f-fe69c9b1c68b", "19fc3854-bee0-4bd0-b4b0-3393cb784090", null });

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "376e968d-ed6b-406e-abcb-f7561f68372d",
                column: "DateCreated",
                value: new DateTime(2021, 9, 8, 23, 26, 2, 334, DateTimeKind.Utc).AddTicks(2225));

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "e03fb776-6a0a-4423-a8b7-24c57570be24",
                column: "DateCreated",
                value: new DateTime(2021, 9, 8, 23, 26, 2, 334, DateTimeKind.Utc).AddTicks(5814));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Signature",
                table: "AspNetUsers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dcb05a3-4924-46b8-b561-0211d7473471",
                columns: new[] { "ConcurrencyStamp", "GroupId" },
                values: new object[] { "2380b2a7-5350-4cd4-8a31-7e83a98ac5ce", "46543f48-2305-48df-a8f3-b8278b1ffbb4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dae749f-5550-46db-9d82-409b76a67554",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp", "Signature" },
                values: new object[] { "cbb99797-1305-4de1-a777-bb4a90729059", "c7315eb4-a915-464d-b740-5b7e4c7e821b", (byte)0 });

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "376e968d-ed6b-406e-abcb-f7561f68372d",
                column: "DateCreated",
                value: new DateTime(2021, 9, 2, 12, 15, 21, 327, DateTimeKind.Utc).AddTicks(3153));

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "e03fb776-6a0a-4423-a8b7-24c57570be24",
                column: "DateCreated",
                value: new DateTime(2021, 9, 2, 12, 15, 21, 327, DateTimeKind.Utc).AddTicks(5247));
        }
    }
}
