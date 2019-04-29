using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManagementSystems.Data.Migrations
{
    public partial class PhoneFieldChange_payroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Phone",
                table: "Employee",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "Employee",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
