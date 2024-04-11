using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure_Layer.Migrations
{
    /// <inheritdoc />
    public partial class CourseDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseModel",
                columns: table => new
                {
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryOrSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LevelOfDifficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceOrPriceModel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrolmentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    AverageRating = table.Column<double>(type: "float", nullable: true),
                    ThumbnailOrImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModificationTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prerequisites = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseIsPublic = table.Column<bool>(type: "bit", nullable: false),
                    CourseIsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IssueCertificate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModel", x => x.CourseId);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08260479-52a0-4c0e-a588-274101a2c3be",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4154af81-575b-4373-b481-8e9a189c8797", "AQAAAAIAAYagAAAAEMG+6sW01ln3gWe2mupmuxgGL902d8MMzoW3OTy4nl53EzBXaD5nf0FXK5/Mt8DvUw==", "bbe0db6b-9d24-41f2-880a-f0d69fbbc961" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseModel");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "08260479-52a0-4c0e-a588-274101a2c3be",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "da87db29-1279-4aba-8ebe-f700eaa445b5", "AQAAAAIAAYagAAAAEFgE/nV1Hw7Q+sAwkPbMqcu6QP8n386ZR0ow7guLl2ZNtYSXfahpwYGCqhx41pvLTQ==", "71b1d3b5-3f8e-4e21-866b-85de090a96e9" });
        }
    }
}
