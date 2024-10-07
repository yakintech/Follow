﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Follow.Data.Migrations
{
    /// <inheritdoc />
    public partial class categoryTableAddDescriptionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BlogCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "BlogCategories");
        }
    }
}
