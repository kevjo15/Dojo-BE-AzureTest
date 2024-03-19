using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_Layer.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036868",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49af0b66-2a2a-48db-ad3a-1b5e1509430a", "AQAAAAIAAYagAAAAEIg+Mugq9dUpctQrbiSVGpbUkgHQ0IJiihiLEQiv8yBiKKn0b2AQYcwMlhLF4TC2IA==", "8529d701-4fc5-4f19-8ea7-dc1e45f0c782" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036869",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce198e2e-a884-4d7f-80b8-142f404edf45", "AQAAAAIAAYagAAAAEFh+v39m41h8MDNY1nRAxNTv2wRD1d6dTbkehL8UaubDmDHazKva2zI28fqr2eZWpA==", "828123a2-c638-4c7f-8bfc-dc2f03faf5e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08260479-52a0-4c0e-a588-274101a2c3be",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37d82e24-b243-46c5-974a-c1d354ee7d02", "AQAAAAIAAYagAAAAEIuV3O0ZDbbBXhKwkYRl4QbGaRyaTL0g3ffFEWcWmcgfihXXxlYBgVwL6YGg8ijsBg==", "95abde3b-e553-4bc5-a068-0a560eace5a4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036868",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "86fecaea-1702-4d73-9782-a7c260d2a4de", "Elliot123!", "0e6fce02-48a5-4bad-b75a-80e848c51055" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036869",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb7b1c96-0344-4d68-adb2-9f686fb7f27a", "Kevin123!", "ba3834c8-fb05-4f19-96ee-e40570f60c7f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08260479-52a0-4c0e-a588-274101a2c3be",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8562e9f1-e27f-4d04-89ed-5500fa3ff17f", "Bojan123!", "2191d964-65ad-4d58-a2d4-9c5f73f7b319" });
        }
    }
}
