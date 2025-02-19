using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class Migration001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branchs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branchs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    Cancelled = table.Column<bool>(type: "boolean", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false),
                    PercentageDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    GrossTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Branchs_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branchs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesItens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    SaleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesItens_Products_SaleId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesItens_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UN_Branchs_Name",
                table: "Branchs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UN_Customers_Name",
                table: "Customers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BranchId",
                table: "Sales",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "UN_Sales_Number",
                table: "Sales",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalesItens_ProductId",
                table: "SalesItens",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesItens_SaleId",
                table: "SalesItens",
                column: "SaleId");

            migrationBuilder.InsertData("Products",
                columns: new[] { "Id", "Name", "UnitPrice" },
                values: new object[,] {
                   { new Guid("474ba4e1-8594-4bad-a1f2-97475903447e"), "Pen", (decimal)10 },
                   { new Guid("5a13f2af-b27d-45d2-bbf7-9fd4164b8e65"), "Pencil",(decimal)11 },
                   { new Guid("996ab2cd-5485-4eaa-aa67-05c7311ce7d2"), "Rubber", (decimal)12 },
                   { new Guid("6ca2372a-f42b-4d14-b72b-ee78393845c2"), "Glue", (decimal)13 }});

            migrationBuilder.InsertData("Customers",
                columns: new[] { "Id", "Name", "Email" },
                values: new object[,] {
                   { new Guid("dbe32999-411b-48a5-af47-0a4b6eeeab8b"), "Ruth J. Cochran", "ruth_j_cochran@gmail.com" },
                   { new Guid("bc34a545-50cc-4e6d-9609-ede40eca3cca"), "Lavada M. Reynolds", "lavada_m_reynolds@gmail.com" },
                   { new Guid("78202e40-7705-45ed-9234-ce7c9b1287ad"), "Helen Z. Hall", "helen_z_hall@gmail.com" }});

            migrationBuilder.InsertData("Branchs",
                columns: new[] { "Id", "Name" },
                values: new object[,] {
                   { new Guid("38530b7e-7fb2-4797-ace6-2f47b8c50449"), "Sede" },
                   { new Guid("63d351c3-db59-4007-8489-7608e6f9d06c"), "Filial 1" }});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesItens");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Branchs");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
