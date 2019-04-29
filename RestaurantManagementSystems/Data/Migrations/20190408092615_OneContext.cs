using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManagementSystems.Data.Migrations
{
    public partial class OneContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItem",
                columns: table => new
                {
                    MenuID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Ingridents = table.Column<string>(maxLength: 500, nullable: true),
                    recipe = table.Column<string>(maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 500, nullable: true),
                    TodaysMenu = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItem", x => x.MenuID);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonName = table.Column<string>(maxLength: 50, nullable: true),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    PaymentType = table.Column<int>(nullable: true),
                    PaymentMode = table.Column<int>(nullable: true),
                    Orderstatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    RecordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CartId = table.Column<string>(nullable: true),
                    MenuItemId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_Carts_MenuItem_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItem",
                        principalColumn: "MenuID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderID = table.Column<int>(nullable: true),
                    MenuItemID = table.Column<int>(nullable: true),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_OrderDetail_MenuItem_MenuItemID",
                        column: x => x.MenuItemID,
                        principalTable: "MenuItem",
                        principalColumn: "MenuID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_MenuItemId",
                table: "Carts",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_MenuItemID",
                table: "OrderDetail",
                column: "MenuItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderID",
                table: "OrderDetail",
                column: "OrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "MenuItem");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
