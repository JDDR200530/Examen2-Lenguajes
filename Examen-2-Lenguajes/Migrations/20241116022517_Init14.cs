using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_2_Lenguajes.Migrations
{
    /// <inheritdoc />
    public partial class Init14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_AspNetUsers_id_user",
                schema: "dbo",
                table: "Partidas");

            migrationBuilder.DropIndex(
                name: "IX_Partidas_id_user",
                schema: "dbo",
                table: "Partidas");

            migrationBuilder.AlterColumn<string>(
                name: "id_user",
                schema: "dbo",
                table: "Partidas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "id_user",
                schema: "dbo",
                table: "Partidas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Partidas_id_user",
                schema: "dbo",
                table: "Partidas",
                column: "id_user");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_AspNetUsers_id_user",
                schema: "dbo",
                table: "Partidas",
                column: "id_user",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
