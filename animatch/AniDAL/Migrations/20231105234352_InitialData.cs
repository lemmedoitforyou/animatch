using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AniDAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Imdbrate = table.Column<double>(type: "double precision", nullable: false),
                    Photo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Photo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    WatchedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimeGenre",
                columns: table => new
                {
                    AnimeId = table.Column<int>(type: "integer", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_AnimeGenre_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeGenre_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddedAnime",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AnimeId = table.Column<int>(type: "integer", nullable: false),
                    UserInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_AddedAnime_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddedAnime_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DislikedAnime",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AnimeId = table.Column<int>(type: "integer", nullable: false),
                    UserInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_DislikedAnime_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DislikedAnime_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikedAnime",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AnimeId = table.Column<int>(type: "integer", nullable: false),
                    UserInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_LikedAnime_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikedAnime_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AnimeId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    UserInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WatchedAnime",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AnimeId = table.Column<int>(type: "integer", nullable: false),
                    UserInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_WatchedAnime_Anime_AnimeId",
                        column: x => x.AnimeId,
                        principalTable: "Anime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchedAnime_UserInfo_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "UserInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddedAnime_AnimeId",
                table: "AddedAnime",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_AddedAnime_UserInfoId",
                table: "AddedAnime",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeGenre_AnimeId",
                table: "AnimeGenre",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeGenre_GenreId",
                table: "AnimeGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_DislikedAnime_AnimeId",
                table: "DislikedAnime",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_DislikedAnime_UserInfoId",
                table: "DislikedAnime",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedAnime_AnimeId",
                table: "LikedAnime",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_LikedAnime_UserInfoId",
                table: "LikedAnime",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_AnimeId",
                table: "Review",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserInfoId",
                table: "Review",
                column: "UserInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchedAnime_AnimeId",
                table: "WatchedAnime",
                column: "AnimeId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchedAnime_UserInfoId",
                table: "WatchedAnime",
                column: "UserInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddedAnime");

            migrationBuilder.DropTable(
                name: "AnimeGenre");

            migrationBuilder.DropTable(
                name: "DislikedAnime");

            migrationBuilder.DropTable(
                name: "LikedAnime");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "WatchedAnime");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Anime");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
