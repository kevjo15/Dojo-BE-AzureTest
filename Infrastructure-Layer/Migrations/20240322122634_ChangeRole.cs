using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_Layer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036868",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Role", "SecurityStamp" },
                values: new object[] { "00de1967-c502-4e0a-b61a-a7899ac8e361", "AQAAAAIAAYagAAAAELU+eUteyYSgSUBphixAlIdaAjbOJHoS2jb9TZcgomW6b9a5KzSoNIH2SyVkQgXTxg==", "Student", "af5336b8-f5b6-4007-a7e8-a60f17bca9ec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036869",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Role", "SecurityStamp" },
                values: new object[] { "69f065c1-8d0f-425a-9831-ca55b44c9e1d", "AQAAAAIAAYagAAAAEMXbGEahO4D3hBE4k2C/s0W/e+CS3585HnL0I2PsGmpHcsMcUIffCtMSX5Rm/T6oag==", "Teacher", "4282b0d9-87c2-4b30-abcc-95ed0369f885" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08260479-52a0-4c0e-a588-274101a2c3be",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Role", "SecurityStamp" },
                values: new object[] { "8704e296-8b99-41a1-95ae-8a6c7b0a046c", "AQAAAAIAAYagAAAAEKUkavLJDddb1CJvDToMj7bg308M4P3qqq6vGAmhi5rboeYxdLCxQPzQLuIILXNGYg==", "Admin", "b5bf7040-c572-4a1d-9075-8e9c994f7f7b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036868",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Role", "SecurityStamp" },
                values: new object[] { "53a92c1b-da11-4d89-be29-ee6484ef7b22", "AQAAAAIAAYagAAAAEEek5jV2J8jUcYO6+4F6/z6gaQmZbUjJ73REoznCfQccr5RsDuUMxyKHrxxtD5cEqg==", "User", "a0da0960-c304-4495-b4b7-5111a9c8cb4e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036869",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Role", "SecurityStamp" },
                values: new object[] { "07b3f918-5930-4009-8569-65fa2fd8065b", "AQAAAAIAAYagAAAAEIA1M+ZZ2LkA2MfmAdniQ7J9a6ZkcIBd1w1eq9Ka0uZnDAcartXHFCHloHbGRAvO6Q==", "User", "c1625285-b4dd-47cf-9f2d-95b743b5b6cb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08260479-52a0-4c0e-a588-274101a2c3be",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Role", "SecurityStamp" },
                values: new object[] { "2f6d74c2-3749-478a-a94c-803118256517", "AQAAAAIAAYagAAAAEA/ZFOc5ImSp1zVXZt/pdZln4D8M5wBdXgmD/9vImmeZOXEgxNY8emKV0gpNfeGG3w==", "User", "679ead22-eaa6-4954-aa38-49eba4088347" });
        }
    }
}
