using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class RemoveEmailAddressFromUsertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmialAddress",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dcb05a3-4924-46b8-b561-0211d7473471",
                columns: new[] { "ConcurrencyStamp", "GroupId" },
                values: new object[] { "502ea4f0-4306-44a3-9e60-b970c8b7b0ec", "95f40909-e16e-4494-9a91-09f786a7f06c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dae749f-5550-46db-9d82-409b76a67554",
                columns: new[] { "ConcurrencyStamp", "Email", "SecurityStamp" },
                values: new object[] { "d9fc60a7-6945-471e-a423-6797a0c96376", "user@gmail.com", "d285bbb6-ee7e-49f1-97bb-03b45be2894f" });

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "376e968d-ed6b-406e-abcb-f7561f68372d",
                column: "DateCreated",
                value: new DateTime(2021, 9, 9, 0, 6, 50, 595, DateTimeKind.Utc).AddTicks(9715));

            migrationBuilder.UpdateData(
                table: "MailConfigurations",
                keyColumn: "Id",
                keyValue: "e03fb776-6a0a-4423-a8b7-24c57570be24",
                column: "DateCreated",
                value: new DateTime(2021, 9, 9, 0, 6, 50, 596, DateTimeKind.Utc).AddTicks(4813));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmialAddress",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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
                columns: new[] { "ConcurrencyStamp", "Email", "EmialAddress", "SecurityStamp" },
                values: new object[] { "1d5dde15-447a-4611-a33f-fe69c9b1c68b", null, "user@gmail.com", "19fc3854-bee0-4bd0-b4b0-3393cb784090" });

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
    }
}
