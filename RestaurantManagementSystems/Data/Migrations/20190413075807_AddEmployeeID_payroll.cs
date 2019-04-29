using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManagementSystems.Data.Migrations
{
    public partial class AddEmployeeID_payroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependent_Employee_EmployeeID",
                table: "Dependent");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "Dependent",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dependent_Employee_EmployeeID",
                table: "Dependent",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependent_Employee_EmployeeID",
                table: "Dependent");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "Dependent",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Dependent_Employee_EmployeeID",
                table: "Dependent",
                column: "EmployeeID",
                principalTable: "Employee",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
