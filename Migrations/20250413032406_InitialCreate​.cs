using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lexify.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate​ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    WordID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WordText = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    WordType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Language = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ColorCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    LearningStatus = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    DateAdded = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.WordID);
                });

            migrationBuilder.CreateTable(
                name: "Definitions",
                columns: table => new
                {
                    DefinitionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WordID = table.Column<int>(type: "INTEGER", nullable: false),
                    DefinitionText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Definitions", x => x.DefinitionID);
                    table.ForeignKey(
                        name: "FK_Definitions_Words_WordID",
                        column: x => x.WordID,
                        principalTable: "Words",
                        principalColumn: "WordID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    ExampleID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WordID = table.Column<int>(type: "INTEGER", nullable: false),
                    ExampleText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.ExampleID);
                    table.ForeignKey(
                        name: "FK_Examples_Words_WordID",
                        column: x => x.WordID,
                        principalTable: "Words",
                        principalColumn: "WordID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WordRelations",
                columns: table => new
                {
                    RelationID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WordID = table.Column<int>(type: "INTEGER", nullable: false),
                    RelatedWordID = table.Column<int>(type: "INTEGER", nullable: false),
                    RelationType = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordRelations", x => x.RelationID);
                    table.ForeignKey(
                        name: "FK_WordRelations_Words_RelatedWordID",
                        column: x => x.RelatedWordID,
                        principalTable: "Words",
                        principalColumn: "WordID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WordRelations_Words_WordID",
                        column: x => x.WordID,
                        principalTable: "Words",
                        principalColumn: "WordID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Definitions_WordID",
                table: "Definitions",
                column: "WordID");

            migrationBuilder.CreateIndex(
                name: "IX_Examples_WordID",
                table: "Examples",
                column: "WordID");

            migrationBuilder.CreateIndex(
                name: "IX_WordRelations_RelatedWordID",
                table: "WordRelations",
                column: "RelatedWordID");

            migrationBuilder.CreateIndex(
                name: "IX_WordRelations_WordID",
                table: "WordRelations",
                column: "WordID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Definitions");

            migrationBuilder.DropTable(
                name: "Examples");

            migrationBuilder.DropTable(
                name: "WordRelations");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
