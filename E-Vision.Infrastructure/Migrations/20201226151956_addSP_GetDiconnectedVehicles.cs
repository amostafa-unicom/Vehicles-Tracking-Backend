using E_Vision.Infrastructure.Context.SqlResourceReader;
using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Vision.Infrastructure.Migrations
{
    public partial class addSP_GetDiconnectedVehicles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(SqlResourceReader.CreateSQLQuary(@"StoredProcedures\SP_DiconnectedVehicles.sql"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
