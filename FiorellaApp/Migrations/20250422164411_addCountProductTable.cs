using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FiorellaApp.Migrations
{
    /// <inheritdoc />
    public partial class addCountProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Blogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 4, 22, 20, 44, 10, 791, DateTimeKind.Local).AddTicks(7322),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 4, 21, 11, 11, 26, 279, DateTimeKind.Local).AddTicks(4868));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "Blogs",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 4, 21, 11, 11, 26, 279, DateTimeKind.Local).AddTicks(4868),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 4, 22, 20, 44, 10, 791, DateTimeKind.Local).AddTicks(7322));
        }
    }
}
