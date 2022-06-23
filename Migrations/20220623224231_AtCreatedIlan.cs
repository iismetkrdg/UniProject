using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduProject.Migrations
{
    public partial class AtCreatedIlan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "atCreated",
                table: "Ilan",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "atCreated",
                table: "Ilan");
        }
    }
}
