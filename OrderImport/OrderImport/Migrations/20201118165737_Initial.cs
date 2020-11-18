using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderImport.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Customers",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(100)", maxLength: 100, nullable: false),
                    CreditLimit = table.Column<decimal>("decimal(8,2)", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Customers", x => x.Id); });

            migrationBuilder.CreateTable(
                "Orders",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>("datetime2", nullable: false),
                    OrderValue = table.Column<decimal>("decimal(8,2)", nullable: false),
                    CustomerId = table.Column<int>("int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        "FK_Orders_Customers_CustomerId",
                        x => x.CustomerId,
                        "Customers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Orders_CustomerId",
                "Orders",
                "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Orders");

            migrationBuilder.DropTable(
                "Customers");
        }
    }
}