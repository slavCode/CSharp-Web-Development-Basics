using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShopHierarchy.Migrations
{
    public partial class CreateItemReviewRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ItemId",
                table: "Reviews",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Items_ItemId",
                table: "Reviews",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Items_ItemId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ItemId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Reviews");
        }
    }
}
