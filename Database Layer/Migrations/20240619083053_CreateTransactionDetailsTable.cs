using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Layer.Migrations
{
    /// <inheritdoc />
    public partial class CreateTransactionDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
           name: "TransactionDetails",
           columns: table => new
           {
               TransactionDetailId = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:Identity", "1, 1"),
               TransactionId = table.Column<int>(nullable: false),
               ProductId = table.Column<int>(nullable: false),
               Quantity = table.Column<int>(nullable: false),
               Price = table.Column<decimal>(nullable: false)
           },
           constraints: table =>
           {
               table.PrimaryKey("PK_TransactionDetails", x => x.TransactionDetailId);
               table.ForeignKey(
                   name: "FK_TransactionDetails_Transactions_TransactionId",
                   column: x => x.TransactionId,
                   principalTable: "Transactions",
                   principalColumn: "TransactionId",
                   onDelete: ReferentialAction.Cascade);
               table.ForeignKey(
                   name: "FK_TransactionDetails_Products_ProductId",
                   column: x => x.ProductId,
                   principalTable: "Products",
                   principalColumn: "ProductId",
                   onDelete: ReferentialAction.Cascade);
           });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_TransactionId",
                table: "TransactionDetails",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDetails_ProductId",
                table: "TransactionDetails",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
      name: "TransactionDetails");
        }
    }
}
