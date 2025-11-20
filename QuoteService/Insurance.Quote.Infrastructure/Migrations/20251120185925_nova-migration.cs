using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Insurance.Quote.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class novamigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Customers_CustomerId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Products_ProductId",
                table: "Quotes");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Customers_CustomerId",
                table: "Quotes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Products_ProductId",
                table: "Quotes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Customers_CustomerId",
                table: "Quotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Products_ProductId",
                table: "Quotes");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Customers_CustomerId",
                table: "Quotes",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Products_ProductId",
                table: "Quotes",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
