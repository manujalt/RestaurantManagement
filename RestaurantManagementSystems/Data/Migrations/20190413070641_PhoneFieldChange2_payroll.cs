using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManagementSystems.Data.Migrations
{
    public partial class PhoneFieldChange2_payroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PhoneNo",
                table: "Dependent",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhoneNo",
                table: "Dependent",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
