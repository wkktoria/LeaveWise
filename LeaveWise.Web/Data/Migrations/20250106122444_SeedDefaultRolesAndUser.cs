using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveWise.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultRolesAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6cfb5108-b1b4-42d1-b19f-0bbcefa89133", null, "Supervisor", "SUPERVISOR" },
                    { "b1c82a14-d653-4fa0-9a3f-dbb0acae2f4c", null, "Administrator", "ADMINISTRATOR" },
                    { "d80ab5af-7746-4240-bc95-aecbd758d97a", null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1b36479a-64e0-48aa-983c-73f4fbd78a08", 0, "cf44f5dc-5950-40ad-80a0-9affbc2f040e", "admin@leavewise.com", true, false, null, "ADMIN@LEAVEWISE.COM", "ADMIN@LEAVEWISE.COM", "AQAAAAIAAYagAAAAEPejU8Px6JR3MOshuJxS0Xv2mKpCWaoqf9OsdWFVylduomkp3pu7Jw8BeSvByMuT7A==", null, false, "f93c64bc-b493-4e16-9b00-d47bb0fed0b8", false, "admin@leavewise.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "b1c82a14-d653-4fa0-9a3f-dbb0acae2f4c", "1b36479a-64e0-48aa-983c-73f4fbd78a08" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cfb5108-b1b4-42d1-b19f-0bbcefa89133");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d80ab5af-7746-4240-bc95-aecbd758d97a");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "b1c82a14-d653-4fa0-9a3f-dbb0acae2f4c", "1b36479a-64e0-48aa-983c-73f4fbd78a08" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1c82a14-d653-4fa0-9a3f-dbb0acae2f4c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1b36479a-64e0-48aa-983c-73f4fbd78a08");
        }
    }
}
