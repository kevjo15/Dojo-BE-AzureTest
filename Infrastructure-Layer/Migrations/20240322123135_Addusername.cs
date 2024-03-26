using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_Layer.Migrations
{
    /// <inheritdoc />
    public partial class Addusername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036868",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "1aa8798d-664e-4b95-b615-8d9696cf5e73", "AQAAAAIAAYagAAAAENa5HDuudWC9hFk54rtpGTNTSirQ+QP6AMkW9Cqlw8WBFnOReyD8Rx1zoAVl+JjZ7Q==", "b104e4bc-22e1-467f-88f2-6ded42fb9b65", "elliot@infinet.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036869",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "91b702fe-fe07-4ab5-8b17-f76923b998db", "AQAAAAIAAYagAAAAEFlW23xmJMoRbr3sOzGn16hpxFkmJdF8OO1wV82+w9IAJT6uER+iqZx1NrjkcBXKBg==", "1467828b-f609-4414-9e68-5325d9a5d772", "kevin@infinet" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08260479-52a0-4c0e-a588-274101a2c3be",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "00418d72-966a-4daf-b323-d23b67df58b2", "AQAAAAIAAYagAAAAENVMc3FZbZ8fAoU9Bu5UTYsLoZ2c2roa0+Bmv9RnMR/EU/wd7xMWiq6IJ+0OmickRA==", "dc4fba9f-1688-4c90-8336-dc51b82c82bf", "bojan@infinet.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036868",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "00de1967-c502-4e0a-b61a-a7899ac8e361", "AQAAAAIAAYagAAAAELU+eUteyYSgSUBphixAlIdaAjbOJHoS2jb9TZcgomW6b9a5KzSoNIH2SyVkQgXTxg==", "af5336b8-f5b6-4007-a7e8-a60f17bca9ec", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036869",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "69f065c1-8d0f-425a-9831-ca55b44c9e1d", "AQAAAAIAAYagAAAAEMXbGEahO4D3hBE4k2C/s0W/e+CS3585HnL0I2PsGmpHcsMcUIffCtMSX5Rm/T6oag==", "4282b0d9-87c2-4b30-abcc-95ed0369f885", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08260479-52a0-4c0e-a588-274101a2c3be",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "8704e296-8b99-41a1-95ae-8a6c7b0a046c", "AQAAAAIAAYagAAAAEKUkavLJDddb1CJvDToMj7bg308M4P3qqq6vGAmhi5rboeYxdLCxQPzQLuIILXNGYg==", "b5bf7040-c572-4a1d-9075-8e9c994f7f7b", null });
        }
    }
}
