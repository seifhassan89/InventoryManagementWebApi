using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addNewReqType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Users_MangerId",
                table: "Stocks");

            migrationBuilder.AlterColumn<int>(
                name: "MangerId",
                table: "Stocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "RequestTypes",
                columns: new[] { "RequestTypeId", "Name" },
                values: new object[] { 3, "Transfer Request" });

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Users_MangerId",
                table: "Stocks",
                column: "MangerId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Users_MangerId",
                table: "Stocks");

            migrationBuilder.DeleteData(
                table: "RequestTypes",
                keyColumn: "RequestTypeId",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "MangerId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Users_MangerId",
                table: "Stocks",
                column: "MangerId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
