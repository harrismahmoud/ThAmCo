using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ThAmCo.Events.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventName = table.Column<string>(type: "TEXT", nullable: false),
                    EventDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EventType = table.Column<string>(type: "TEXT", nullable: false),
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodBookingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GuestName = table.Column<string>(type: "TEXT", nullable: false),
                    GuestContact = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.GuestId);
                });

            migrationBuilder.CreateTable(
                name: "staffs",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StaffName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffs", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "guestBookings",
                columns: table => new
                {
                    GuestId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guestBookings", x => new { x.EventId, x.GuestId });
                    table.ForeignKey(
                        name: "FK_guestBookings_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_guestBookings_Guests_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guests",
                        principalColumn: "GuestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staffings",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false),
                    StaffRole = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffings", x => new { x.StaffRole, x.EventId });
                    table.ForeignKey(
                        name: "FK_staffings_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_staffings_staffs_EventId",
                        column: x => x.EventId,
                        principalTable: "staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "EventDateTime", "EventName", "EventType", "FoodBookingId", "ReservationId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tech Conference", "Conference", 0, 0 },
                    { 2, new DateTime(2024, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Art Exhibition", "Exhibition", 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "GuestId", "GuestContact", "GuestName" },
                values: new object[,]
                {
                    { 1, 1234567890, "John Doe" },
                    { 2, 12345, "Jane Smith" },
                    { 3, 734567589, "Alice Johnson" },
                    { 4, 1122334455, "Mark Williams" },
                    { 5, 667788990, "Emily Davis" }
                });

            migrationBuilder.InsertData(
                table: "staffs",
                columns: new[] { "StaffId", "Email", "StaffName" },
                values: new object[,]
                {
                    { 1, "alice.brown@company.com", "Alice Brown" },
                    { 2, "bob.white@company.com", "Bob White" }
                });

            migrationBuilder.InsertData(
                table: "guestBookings",
                columns: new[] { "EventId", "GuestId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "staffings",
                columns: new[] { "EventId", "StaffRole" },
                values: new object[,]
                {
                    { 1, "Curator" },
                    { 1, "Event Manager" },
                    { 1, "Security" },
                    { 2, "Security" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_guestBookings_GuestId",
                table: "guestBookings",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_staffings_EventId",
                table: "staffings",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guestBookings");

            migrationBuilder.DropTable(
                name: "staffings");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "staffs");
        }
    }
}
