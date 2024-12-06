using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSouvenirShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIdInVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Vouchers_VoucherCode",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Orders_VoucherCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "VoucherCode",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Vouchers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "VoucherId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VoucherId",
                table: "Orders",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Vouchers_VoucherId",
                table: "Orders",
                column: "VoucherId",
                principalTable: "Vouchers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Vouchers_VoucherId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers");

            migrationBuilder.DropIndex(
                name: "IX_Orders_VoucherId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Vouchers");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "VoucherCode",
                table: "Orders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_VoucherCode",
                table: "Orders",
                column: "VoucherCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Vouchers_VoucherCode",
                table: "Orders",
                column: "VoucherCode",
                principalTable: "Vouchers",
                principalColumn: "Code");
        }
    }
}
