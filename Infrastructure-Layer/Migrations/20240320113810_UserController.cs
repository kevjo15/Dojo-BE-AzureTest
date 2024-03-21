using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_Layer.Migrations
{
    /// <inheritdoc />
    public partial class UserController : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036868",
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bccafb4-26a3-49a9-a834-0732fc8d9169", false, "AQAAAAIAAYagAAAAEMJ4VDiT125nIb3DIG4mT3zBYF/lImfwW1MvF6mdN4PXtxtstSbrQ3MCkfTCWc9Mqg==", "fd65af21-eea5-4baa-8840-c0ea4f72ecf8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036869",
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "100f3a3e-48b3-4738-af1a-4f1e4e064f4f", false, "AQAAAAIAAYagAAAAEAOA0PkCleWhd2Xs57nkGMF9i41ChVLziLvAkBusxaNHPpquGOGRZjb/UyyvXGQUGw==", "8c634df8-3ac6-4733-b950-4bc44b1277fe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08260479-52a0-4c0e-a588-274101a2c3be",
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb9ea46d-5286-4e99-abe6-b0b315bbe821", false, "AQAAAAIAAYagAAAAENTnlC1z+W/MRUTiqTVxMDzfCKcm0gmb96tq4weN1T8fO4fgBmYCHrGXo0SR7x6BRg==", "b45c3164-024c-4fd4-bc6c-b63f94deefbd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

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
    }
}
