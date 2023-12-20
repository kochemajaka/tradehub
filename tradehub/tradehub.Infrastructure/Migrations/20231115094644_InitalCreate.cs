using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tradehub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuyCars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyedOn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyCars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuyoutCars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyedOn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Profit = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyoutCars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrepareCars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrepareCars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleCars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleCars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DDSs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Account = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsProfit = table.Column<bool>(type: "bit", nullable: false),
                    BuyCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrepareCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DDSs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DDSs_BuyCars_BuyCarId",
                        column: x => x.BuyCarId,
                        principalTable: "BuyCars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DDSs_PrepareCars_PrepareCarId",
                        column: x => x.PrepareCarId,
                        principalTable: "PrepareCars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DDSs_SaleCars_SaleCarId",
                        column: x => x.SaleCarId,
                        principalTable: "SaleCars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuyoutCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrepareCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_BuyCars_BuyCarId",
                        column: x => x.BuyCarId,
                        principalTable: "BuyCars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_BuyoutCars_BuyoutCarId",
                        column: x => x.BuyoutCarId,
                        principalTable: "BuyoutCars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_PrepareCars_PrepareCarId",
                        column: x => x.PrepareCarId,
                        principalTable: "PrepareCars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employees_SaleCars_SaleCarId",
                        column: x => x.SaleCarId,
                        principalTable: "SaleCars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BuyoutCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DDSId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrepareCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SaleCarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_BuyCars_BuyCarId",
                        column: x => x.BuyCarId,
                        principalTable: "BuyCars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_BuyoutCars_BuyoutCarId",
                        column: x => x.BuyoutCarId,
                        principalTable: "BuyoutCars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_DDSs_DDSId",
                        column: x => x.DDSId,
                        principalTable: "DDSs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_PrepareCars_PrepareCarId",
                        column: x => x.PrepareCarId,
                        principalTable: "PrepareCars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cars_SaleCars_SaleCarId",
                        column: x => x.SaleCarId,
                        principalTable: "SaleCars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BuyCarId",
                table: "Cars",
                column: "BuyCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BuyoutCarId",
                table: "Cars",
                column: "BuyoutCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_DDSId",
                table: "Cars",
                column: "DDSId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_JobId",
                table: "Cars",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_PrepareCarId",
                table: "Cars",
                column: "PrepareCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_SaleCarId",
                table: "Cars",
                column: "SaleCarId");

            migrationBuilder.CreateIndex(
                name: "IX_DDSs_BuyCarId",
                table: "DDSs",
                column: "BuyCarId");

            migrationBuilder.CreateIndex(
                name: "IX_DDSs_PrepareCarId",
                table: "DDSs",
                column: "PrepareCarId");

            migrationBuilder.CreateIndex(
                name: "IX_DDSs_SaleCarId",
                table: "DDSs",
                column: "SaleCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BuyCarId",
                table: "Employees",
                column: "BuyCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BuyoutCarId",
                table: "Employees",
                column: "BuyoutCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobId",
                table: "Employees",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PrepareCarId",
                table: "Employees",
                column: "PrepareCarId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SaleCarId",
                table: "Employees",
                column: "SaleCarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "DDSs");

            migrationBuilder.DropTable(
                name: "BuyoutCars");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "BuyCars");

            migrationBuilder.DropTable(
                name: "PrepareCars");

            migrationBuilder.DropTable(
                name: "SaleCars");
        }
    }
}
