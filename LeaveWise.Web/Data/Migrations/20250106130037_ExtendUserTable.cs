using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveWise.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1b36479a-64e0-48aa-983c-73f4fbd78a08",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0fd3c53-2997-4758-90af-9b9d083491fb", new DateOnly(1999, 1, 1), "Admin", "Default", "AQAAAAIAAYagAAAAEFglov7xbOtq1GTJ4xyYz0GDEr2deGXtETECtUT5GDbdzGWXfIikLEz/YjpehXTcww==", "88d7d77b-b56d-4d1d-b8b6-dc4c457da2df" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1b36479a-64e0-48aa-983c-73f4fbd78a08",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf44f5dc-5950-40ad-80a0-9affbc2f040e", "AQAAAAIAAYagAAAAEPejU8Px6JR3MOshuJxS0Xv2mKpCWaoqf9OsdWFVylduomkp3pu7Jw8BeSvByMuT7A==", "f93c64bc-b493-4e16-9b00-d47bb0fed0b8" });
        }
    }
}
