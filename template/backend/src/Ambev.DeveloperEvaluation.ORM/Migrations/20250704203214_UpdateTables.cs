using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchName",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "SaleItems");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Sales",
                newName: "CustomerExternalId");

            migrationBuilder.RenameColumn(
                name: "BranchId",
                table: "Sales",
                newName: "BranchExternalId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "SaleItems",
                newName: "ProductExternalId");

            migrationBuilder.AlterColumn<Guid>(
                name: "SaleId",
                table: "SaleItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerExternalId",
                table: "Sales",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "BranchExternalId",
                table: "Sales",
                newName: "BranchId");

            migrationBuilder.RenameColumn(
                name: "ProductExternalId",
                table: "SaleItems",
                newName: "ProductId");

            migrationBuilder.AddColumn<string>(
                name: "BranchName",
                table: "Sales",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Sales",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AlterColumn<Guid>(
                name: "SaleId",
                table: "SaleItems",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "SaleItems",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: string.Empty);
        }
    }
}
