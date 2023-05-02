using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wenlin.IDP.Migrations
{
    public partial class AddAccountActivation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("29aae5d7-9a74-41a6-95d9-1e4742a91e05"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("52ca1018-5030-4d2e-a5a0-0c0cf3fa80e7"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("74f07642-e27f-43a7-9dc0-fa550e45c702"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("abdbc9d7-5cb8-438d-bc6b-450f4fbb748b"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("add9b618-cc2e-4ff5-8421-39f02988a046"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("e81f6251-b487-4800-865e-6fb7c762c388"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("eb3bd747-e3cb-4651-906e-f5095bb04aa2"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("ed04305e-00da-433e-a3f9-aadd799f1319"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecurityCode",
                table: "Users",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SecurityCodeExpirationDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("21c454e1-c95c-4954-8604-e00d1bc2ccd1"), "63c103bb-013e-4711-b4a3-eefb987c5d76", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" },
                    { new Guid("5c27d54c-37e5-4621-987a-0652efd006f9"), "4178f833-87d2-4f0a-9d54-eadab8b0cc5c", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" },
                    { new Guid("710a73de-a63e-4148-99e4-36f470d8d00f"), "1afb5101-0adb-4974-a31e-6f8d085f93b6", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" },
                    { new Guid("7ff27784-43c4-4bb1-820d-7612505ac918"), "2fa17a87-a495-4088-991c-a3e536193bc0", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" },
                    { new Guid("8c688928-3ca4-471a-afc7-b6677c320133"), "ff5bb99f-3658-44b7-9605-48827b809ac8", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" },
                    { new Guid("9d0f6f45-6117-489c-889b-10e1a2e5eaba"), "4bc77b44-2b54-4411-a0dd-7a4f45469e98", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" },
                    { new Guid("b0117b99-d761-45be-bce2-f805f487aab1"), "58ff3324-b0ac-4934-9ca4-f56443606710", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" },
                    { new Guid("e6bfd28d-3a65-4b89-89a6-be0373db0dc9"), "4bdf3bb6-7b62-47d4-b52b-1c3d7a6e406a", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                columns: new[] { "ConcurrencyStamp", "Email" },
                values: new object[] { "0294c20f-3150-4880-b44f-d114a3cceded", "david@hotmail.com" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                columns: new[] { "ConcurrencyStamp", "Email" },
                values: new object[] { "b62afb0f-bddb-455c-8033-1d79d740d93d", "emma@hotmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("21c454e1-c95c-4954-8604-e00d1bc2ccd1"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("5c27d54c-37e5-4621-987a-0652efd006f9"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("710a73de-a63e-4148-99e4-36f470d8d00f"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("7ff27784-43c4-4bb1-820d-7612505ac918"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("8c688928-3ca4-471a-afc7-b6677c320133"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("9d0f6f45-6117-489c-889b-10e1a2e5eaba"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("b0117b99-d761-45be-bce2-f805f487aab1"));

            migrationBuilder.DeleteData(
                table: "UserClaims",
                keyColumn: "Id",
                keyValue: new Guid("e6bfd28d-3a65-4b89-89a6-be0373db0dc9"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecurityCodeExpirationDate",
                table: "Users");

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"),
                column: "ConcurrencyStamp",
                value: "cfb11b58-3af9-4f1b-ace2-62175980ff8e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"),
                column: "ConcurrencyStamp",
                value: "e7dba94d-065f-4276-b7f9-0c0e0ddb7dcd");
        }
    }
}
