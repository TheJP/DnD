using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DnD.Data.Migrations
{
    public partial class AttributeRework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDragon",
                table: "Races",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Attributes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Special",
                table: "Attributes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitialLife",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InitialMana",
                table: "Characters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDragon",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "Special",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "InitialLife",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "InitialMana",
                table: "Characters");
        }
    }
}
