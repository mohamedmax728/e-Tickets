using Microsoft.EntityFrameworkCore.Migrations;

namespace eTickets.Migrations
{
    public partial class Actor_MovieAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movie_actors_ActorId",
                table: "Actor_Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_Actor_Movie_movies_MovieId",
                table: "Actor_Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Actor_Movie",
                table: "Actor_Movie");

            migrationBuilder.RenameTable(
                name: "Actor_Movie",
                newName: "actor_Movies");

            migrationBuilder.RenameIndex(
                name: "IX_Actor_Movie_MovieId",
                table: "actor_Movies",
                newName: "IX_actor_Movies_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_actor_Movies",
                table: "actor_Movies",
                columns: new[] { "ActorId", "MovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_actor_Movies_actors_ActorId",
                table: "actor_Movies",
                column: "ActorId",
                principalTable: "actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_actor_Movies_movies_MovieId",
                table: "actor_Movies",
                column: "MovieId",
                principalTable: "movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_actor_Movies_actors_ActorId",
                table: "actor_Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_actor_Movies_movies_MovieId",
                table: "actor_Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_actor_Movies",
                table: "actor_Movies");

            migrationBuilder.RenameTable(
                name: "actor_Movies",
                newName: "Actor_Movie");

            migrationBuilder.RenameIndex(
                name: "IX_actor_Movies_MovieId",
                table: "Actor_Movie",
                newName: "IX_Actor_Movie_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Actor_Movie",
                table: "Actor_Movie",
                columns: new[] { "ActorId", "MovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movie_actors_ActorId",
                table: "Actor_Movie",
                column: "ActorId",
                principalTable: "actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Actor_Movie_movies_MovieId",
                table: "Actor_Movie",
                column: "MovieId",
                principalTable: "movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
