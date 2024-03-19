using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure_Layer.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "047425eb-15a5-4310-9d25-e281ab036868", 0, "86fecaea-1702-4d73-9782-a7c260d2a4de", "UserModel", "elliot@infinet.com", false, "Elliot", "Dahlin", false, null, null, null, "Elliot123!", null, false, "User", "0e6fce02-48a5-4bad-b75a-80e848c51055", false, null },
                    { "047425eb-15a5-4310-9d25-e281ab036869", 0, "cb7b1c96-0344-4d68-adb2-9f686fb7f27a", "UserModel", "kevin@infinet.com", false, "Kevin", "Jorgensen", false, null, null, null, "Kevin123!", null, false, "User", "ba3834c8-fb05-4f19-96ee-e40570f60c7f", false, null },
                    { "08260479-52a0-4c0e-a588-274101a2c3be", 0, "8562e9f1-e27f-4d04-89ed-5500fa3ff17f", "UserModel", "bojan@infinet.com", false, "Bojan", "Mirkovic", false, null, null, null, "Bojan123!", null, false, "User", "2191d964-65ad-4d58-a2d4-9c5f73f7b319", false, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036868");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "047425eb-15a5-4310-9d25-e281ab036869");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08260479-52a0-4c0e-a588-274101a2c3be");
        }
    }
}
