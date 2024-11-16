using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_2_Lenguajes.Migrations
{
    /// <inheritdoc />
    public partial class Init13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_CodigoCuenta",
                table: "Partidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_UserId",
                table: "Partidas");

            migrationBuilder.RenameTable(
                name: "Partidas",
                newName: "Partidas",
                newSchema: "dbo");

            migrationBuilder.AlterColumn<string>(
                name: "tipo_transaccion",
                schema: "dbo",
                table: "Partidas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nombre_cuenta",
                schema: "dbo",
                table: "Partidas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "id_user",
                schema: "dbo",
                table: "Partidas",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                schema: "dbo",
                table: "Partidas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "codigo_cuenta",
                schema: "dbo",
                table: "Partidas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_AspNetUsers_id_user",
                schema: "dbo",
                table: "Partidas",
                column: "id_user",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_Catalago_Cuentas_codigo_cuenta",
                schema: "dbo",
                table: "Partidas",
                column: "codigo_cuenta",
                principalSchema: "dbo",
                principalTable: "Catalago_Cuentas",
                principalColumn: "codigos_cuentas",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_AspNetUsers_id_user",
                schema: "dbo",
                table: "Partidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Partidas_Catalago_Cuentas_codigo_cuenta",
                schema: "dbo",
                table: "Partidas");

            migrationBuilder.RenameTable(
                name: "Partidas",
                schema: "dbo",
                newName: "Partidas");

            migrationBuilder.AlterColumn<string>(
                name: "tipo_transaccion",
                table: "Partidas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "nombre_cuenta",
                table: "Partidas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "id_user",
                table: "Partidas",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "descripcion",
                table: "Partidas",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "codigo_cuenta",
                table: "Partidas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_CodigoCuenta",
                table: "Partidas",
                column: "codigo_cuenta",
                principalSchema: "dbo",
                principalTable: "Catalago_Cuentas",
                principalColumn: "codigos_cuentas",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Partidas_UserId",
                table: "Partidas",
                column: "id_user",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
