using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Books.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 2500, nullable: false),
                    AuthorId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("b290f1ee-6c54-4b01-90e6-d701748f0853"), "JRR", "Tolkien" },
                    { new Guid("c290f1ee-6c54-4b01-90e6-d701748f0852"), "Stephan", "Fry" },
                    { new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851"), "George", "RR Martin" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("a290f1ee-6c54-4b01-90e6-d701748f0853"), new Guid("b290f1ee-6c54-4b01-90e6-d701748f0853"), "Prequel to The Lord of the Rings.", "The Hobbit" },
                    { new Guid("e290f1ee-6c54-4b01-90e6-d701748f0851"), new Guid("d290f1ee-6c54-4b01-90e6-d701748f0851"), "First book in the A Song of Ice and Fire series.", "A Game of Thrones" },
                    { new Guid("f290f1ee-6c54-4b01-90e6-d701748f0852"), new Guid("c290f1ee-6c54-4b01-90e6-d701748f0852"), "First book in The Kingkiller Chronicle series.", "The Name of the Wind" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
