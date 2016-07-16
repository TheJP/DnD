using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DnD.Data.Migrations
{
    public partial class Adventure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adventures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DungeonMasterId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NextId = table.Column<int>(nullable: true),
                    PreviousId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adventures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adventures_AspNetUsers_DungeonMasterId",
                        column: x => x.DungeonMasterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Adventures_Adventures_NextId",
                        column: x => x.NextId,
                        principalTable: "Adventures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdventureParticipations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdventureId = table.Column<int>(nullable: false),
                    AdventurerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureParticipations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdventureParticipations_Adventures_AdventureId",
                        column: x => x.AdventureId,
                        principalTable: "Adventures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdventureParticipations_Characters_AdventurerId",
                        column: x => x.AdventurerId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Characters",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_DungeonMasterId",
                table: "Adventures",
                column: "DungeonMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_NextId",
                table: "Adventures",
                column: "NextId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdventureParticipations_AdventureId",
                table: "AdventureParticipations",
                column: "AdventureId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureParticipations_AdventurerId",
                table: "AdventureParticipations",
                column: "AdventurerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "AdventureParticipations");

            migrationBuilder.DropTable(
                name: "Adventures");

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
