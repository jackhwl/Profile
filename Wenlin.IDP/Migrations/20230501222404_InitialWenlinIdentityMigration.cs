using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wenlin.IDP.Migrations
{
    public partial class InitialWenlinIdentityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Password", "Subject", "UserName" },
                values: new object[] { new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), true, "cfb11b58-3af9-4f1b-ace2-62175980ff8e", "password", "d860efca-22d9-47fd-8249-791ba61b07c7", "David" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Password", "Subject", "UserName" },
                values: new object[] { new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), true, "e7dba94d-065f-4276-b7f9-0c0e0ddb7dcd", "password", "b7539694-97e7-4dfe-84da-b4256e1ff5c7", "Emma" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("29aae5d7-9a74-41a6-95d9-1e4742a91e05"), "307648b7-4707-40e0-87f3-edf8f9eb92c0", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" },
                    { new Guid("52ca1018-5030-4d2e-a5a0-0c0cf3fa80e7"), "f0f3e00e-8f9e-4d6d-8acf-9df5b8f95b0c", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" },
                    { new Guid("74f07642-e27f-43a7-9dc0-fa550e45c702"), "78bcc86e-8c13-42b0-bdb4-8bfd5a24953b", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" },
                    { new Guid("abdbc9d7-5cb8-438d-bc6b-450f4fbb748b"), "6a99f353-29a6-4d15-846c-2f1dda953341", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" },
                    { new Guid("add9b618-cc2e-4ff5-8421-39f02988a046"), "64784ac5-0f92-4e34-842f-5583f0dba7a9", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" },
                    { new Guid("e81f6251-b487-4800-865e-6fb7c762c388"), "8caac4da-2fc9-4c22-8d54-f0c148e632bb", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" },
                    { new Guid("eb3bd747-e3cb-4651-906e-f5095bb04aa2"), "e1c2a473-58d6-404e-9e28-79c436720d82", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" },
                    { new Guid("ed04305e-00da-433e-a3f9-aadd799f1319"), "e5697152-f2d8-4325-ace5-501b67211937", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Subject",
                table: "Users",
                column: "Subject",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
