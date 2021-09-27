using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class MesgTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserGroups",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "groupId",
                table: "UserGroups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NotificationMessageTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NotificationActionType = table.Column<int>(type: "int", nullable: false),
                    MessageTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationMessageTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationReceivers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserEmails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NotificationActionType = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateDeleted = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationReceivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationReceivers_AspNetRoles_GroupId",
                        column: x => x.GroupId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cbb99797-1305-4de1-a777-bb4a90729059", "c7315eb4-a915-464d-b740-5b7e4c7e821b" });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_groupId",
                table: "UserGroups",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_UserId",
                table: "UserGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationReceivers_GroupId",
                table: "NotificationReceivers",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_AspNetRoles_groupId",
                table: "UserGroups",
                column: "groupId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_AspNetRoles_groupId",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups");

            migrationBuilder.DropTable(
                name: "NotificationMessageTemplates");

            migrationBuilder.DropTable(
                name: "NotificationReceivers");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_groupId",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_UserId",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "groupId",
                table: "UserGroups");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
        }
    }
}
