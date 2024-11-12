using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Catering.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodBookings",
                columns: table => new
                {
                    FoodBookingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientReferenceId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodBookings", x => x.FoodBookingId);
                });

            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.FoodItemId);
                });

            migrationBuilder.CreateTable(
                name: "menus",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MenuName = table.Column<string>(type: "TEXT", nullable: false),
                    FoodBookingId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menus", x => x.MenuId);
                    table.ForeignKey(
                        name: "FK_menus_FoodBookings_FoodBookingId",
                        column: x => x.FoodBookingId,
                        principalTable: "FoodBookings",
                        principalColumn: "FoodBookingId");
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodMenuId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => new { x.MenuId, x.FoodMenuId });
                    table.ForeignKey(
                        name: "FK_MenuItems_FoodItems_FoodMenuId",
                        column: x => x.FoodMenuId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItems_menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "menus",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FoodBookings",
                columns: new[] { "FoodBookingId", "ClientReferenceId", "MenuId", "NumberOfGuests" },
                values: new object[,]
                {
                    { 1, 101, 1, 10 },
                    { 2, 102, 2, 15 },
                    { 3, 103, 3, 20 },
                    { 4, 104, 1, 5 },
                    { 5, 105, 2, 8 }
                });

            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "FoodItemId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Cheese and tomato pizza", "Pizza", 12.99 },
                    { 2, "Spaghetti with marinara sauce", "Pasta", 9.9900000000000002 },
                    { 3, "Mixed greens with balsamic dressing", "Salad", 5.9900000000000002 },
                    { 4, "Beef burger with lettuce and tomato", "Burger", 8.9900000000000002 },
                    { 5, "Carbonated drink", "Soda", 1.99 }
                });

            migrationBuilder.InsertData(
                table: "menus",
                columns: new[] { "MenuId", "FoodBookingId", "MenuName" },
                values: new object[,]
                {
                    { 1, null, "Vegetarian Delight" },
                    { 2, null, "Italian Feast" },
                    { 3, null, "Classic Burger Meal" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "FoodMenuId", "MenuId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 4, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 5, 2 },
                    { 4, 3 },
                    { 5, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_FoodMenuId",
                table: "MenuItems",
                column: "FoodMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_menus_FoodBookingId",
                table: "menus",
                column: "FoodBookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "menus");

            migrationBuilder.DropTable(
                name: "FoodBookings");
        }
    }
}
