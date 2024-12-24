using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineSouvenirShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class addListFavoriteProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b6606f95-1f53-4353-b601-7893df7a1618"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c862f812-c971-4da7-a20d-87268ef07755"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("f9c986eb-1239-41fd-b5e3-dc8e4b60cf2e"), null, "Customer", "CUSTOMER" },
                    { new Guid("fb42f173-5175-413f-9334-4909671ad77e"), null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f9c986eb-1239-41fd-b5e3-dc8e4b60cf2e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb42f173-5175-413f-9334-4909671ad77e"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("b6606f95-1f53-4353-b601-7893df7a1618"), null, "Admin", "ADMIN" },
                    { new Guid("c862f812-c971-4da7-a20d-87268ef07755"), null, "Customer", "CUSTOMER" }
                });
        }
    }
}
