using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fora.Server.Migrations.AuthDb
{
    public partial class newAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb77a5c6-5082-4cf9-92ab-aea4c7872c94", true, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEA+2r3P0eEX49AWnfkUCsVMczZJonzSTuE7ktqm11NcPa2sWgfuOD58cW1WX6RVXrw==", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0–9412–4cfe-afbf-59f706d72cf6",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c421943-8c62-40ff-a71d-2ceab7f235dc", false, null, null, "AQAAAAEAACcQAAAAEBB84ds7vHL3L2oOLlDxOt7RaM0XhtklSmALP7+KVm7nKFMRXbPqkZT1f9fOGnuIcA==", "9edfb987-48ce-4ef1-960a-720ff38e208a" });
        }
    }
}
