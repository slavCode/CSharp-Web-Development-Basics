using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ShopHierarchy.Migrations
{
    public partial class FixItemRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrders_Orders_ItemId",
                table: "ItemsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrders_Items_OrderId",
                table: "ItemsOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrders_Items_ItemId",
                table: "ItemsOrders",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrders_Orders_OrderId",
                table: "ItemsOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrders_Items_ItemId",
                table: "ItemsOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrders_Orders_OrderId",
                table: "ItemsOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrders_Orders_ItemId",
                table: "ItemsOrders",
                column: "ItemId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrders_Items_OrderId",
                table: "ItemsOrders",
                column: "OrderId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
