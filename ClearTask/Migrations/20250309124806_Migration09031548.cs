using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClearTask.Migrations
{
    /// <inheritdoc />
    public partial class Migration09031548 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "Proposals");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Proposals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProposalMaterials",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Proposals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Proposals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Division",
                table: "Proposals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Proposals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Proposals",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ProposalMaterials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "ProposalMaterials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                table: "ProposalMaterials",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProposalMaterials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProposalId",
                table: "ProposalMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ProposalMaterials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "ProposalMaterials",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_ProposalMaterials_ProposalId",
                table: "ProposalMaterials",
                column: "ProposalId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProposalMaterials_Proposals_ProposalId",
                table: "ProposalMaterials",
                column: "ProposalId",
                principalTable: "Proposals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProposalMaterials_Proposals_ProposalId",
                table: "ProposalMaterials");

            migrationBuilder.DropIndex(
                name: "IX_ProposalMaterials_ProposalId",
                table: "ProposalMaterials");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "Division",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Proposals");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ProposalMaterials");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "ProposalMaterials");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                table: "ProposalMaterials");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProposalMaterials");

            migrationBuilder.DropColumn(
                name: "ProposalId",
                table: "ProposalMaterials");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProposalMaterials");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProposalMaterials");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Proposals",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProposalMaterials",
                newName: "id");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Proposals",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
