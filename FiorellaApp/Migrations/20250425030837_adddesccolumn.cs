using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiorellaApp.Migrations
{
    /// <inheritdoc />
    public partial class adddesccolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Blogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 4, 25, 7, 8, 36, 868, DateTimeKind.Local).AddTicks(1366),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 4, 22, 20, 44, 10, 791, DateTimeKind.Local).AddTicks(7322));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Categories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Blogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 4, 22, 20, 44, 10, 791, DateTimeKind.Local).AddTicks(7322),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 4, 25, 7, 8, 36, 868, DateTimeKind.Local).AddTicks(1366));
        }
    }
}
