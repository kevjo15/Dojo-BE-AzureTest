using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_Layer.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserDataUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036868",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7a19725b-0c1b-49f3-ab8c-2707dd9bbf45", "AQAAAAIAAYagAAAAEGV9VaM0aMjpy8F5I0mlYO7e78wnuXFZlO0sxD9E5a/5NHV14wpeHO/RTE8OzQcpkQ==", "94675d88-b8bb-49f2-89e6-9c1857e14a97" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036869",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d1e9ecd-0cc9-4c5c-870f-a944c6771946", "AQAAAAIAAYagAAAAEMX0fGem1LA/+wuo5Z0xfhEluNQhY82hqAeMXSsQe80U6aQT7SoF+AF5C/iHEH9pmQ==", "f0d26140-2c0c-493a-ab01-9b85d5b3da4a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08260479-52a0-4c0e-a588-274101a2c3be",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83bde5c0-5753-4c62-a302-f7fd1d6b23b6", "AQAAAAIAAYagAAAAEB47OpMVP/lQuF3HBJKI+jvTmF0O9XCs0EyIqpOyrtAqaQgT16/fD9NhB2HGWQweYA==", "3020c37d-3f2b-460e-b1a5-f2d9fc8d7855" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
