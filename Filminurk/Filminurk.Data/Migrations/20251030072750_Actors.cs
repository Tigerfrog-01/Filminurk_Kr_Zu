using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filminurk.Data.Migrations
{
    /// <inheritdoc />
    public partial class Actors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ActorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoviesActedFor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortraitID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Addiction = table.Column<int>(type: "int", nullable: false),
                    Crimes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ActorID);
                });

            migrationBuilder.CreateTable(
                name: "UserComments",
                columns: table => new
                {
                    CommentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommenterUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentedScore = table.Column<int>(type: "int", nullable: false),
                    IsHelpful = table.Column<int>(type: "int", nullable: false),
                    IsHarmful = table.Column<int>(type: "int", nullable: false),
                    CommentCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommentDeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MovieID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserComments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_UserComments_Movies_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movies",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_MovieID",
                table: "UserComments",
                column: "MovieID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "UserComments");
        }
    }
}
