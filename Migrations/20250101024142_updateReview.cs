using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineSouvenirShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6b1b1841-dd61-4372-a3ba-99ee7a493c98"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c36387d6-96b1-4be4-861e-636aaff6a4e5"));

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Reviews");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b2f8dbdd-a518-4c8d-900f-9149466462f6"), null, "Admin", "ADMIN" },
                    { new Guid("f61eca15-8c09-48f9-bd3c-782283d231ec"), null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b2f8dbdd-a518-4c8d-900f-9149466462f6"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f61eca15-8c09-48f9-bd3c-782283d231ec"));

            migrationBuilder.AddColumn<byte>(
                name: "Rating",
                table: "Reviews",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6b1b1841-dd61-4372-a3ba-99ee7a493c98"), null, "Customer", "CUSTOMER" },
                    { new Guid("c36387d6-96b1-4be4-861e-636aaff6a4e5"), null, "Admin", "ADMIN" }
                });
        }
    }
}
