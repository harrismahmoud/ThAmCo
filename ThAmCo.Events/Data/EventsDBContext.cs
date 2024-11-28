using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ThAmCo.Events.Data
{
    public class EventsDBContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<GuestBooking> guestBookings { get; set; }
        public DbSet<Staff> staffs { get; set; }
        public DbSet<Staffing> staffings { get; set; }
        private string DbPath { get; set; } = string.Empty;


        //Constructor to setup the DB path and name
        public EventsDBContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "Events.db");
        }


        // OnConfiguring to specify that the SQLite database engine will be used
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Event>()
               .HasKey(e => e.EventId);

            builder.Entity<Staff>()
                .HasKey(e => e.StaffId);

            builder.Entity<Staffing>()
                .HasKey(st => new { st.StaffRole, st.EventId });

           

            builder.Entity<Event>()
                .HasMany(st => st.staffing)
                .WithOne(e => e.Event)
                .HasForeignKey(e => e.EventId);

            builder.Entity<Staff>()
                .HasMany(st=> st.staffing)
                .WithOne(st=> st.Staff)
                .HasForeignKey(s => s.EventId);


            builder.Entity<GuestBooking>()
                .HasKey(st => new { st.EventId, st.GuestId });

            builder.Entity<Event>()
                .HasMany(gb => gb.GuestBooking)
                .WithOne(gb => gb.Event)
                .HasForeignKey(s => s.EventId);

            builder.Entity<Guest>()
                .HasMany(gb => gb.GuestBooking)
                .WithOne(g => g.Guest)
                .HasForeignKey(s => s.EventId);







            builder.Entity<Event>().HasData(
           new Event { EventId = 1, EventName = "Tech Conference", EventDateTime = new DateTime(2024, 12, 15), EventType = "Conference" },
           new Event { EventId = 2, EventName = "Art Exhibition", EventDateTime = new DateTime(2024, 12, 22), EventType = "Exhibition" }
       );

            // Seed data for Guests
            builder.Entity<Guest>().HasData(
                new Guest { GuestId = 1, GuestName = "John Doe", GuestContact = 1234567890 },
            new Guest { GuestId = 2, GuestName = "Jane Smith", GuestContact = 012345 },
            new Guest { GuestId = 3, GuestName = "Alice Johnson", GuestContact = 0734567589 },
            new Guest { GuestId = 4, GuestName = "Mark Williams", GuestContact = 1122334455 },
            new Guest { GuestId = 5, GuestName = "Emily Davis", GuestContact = 667788990 }
        );

            // Seed data for GuestBookings
            builder.Entity<GuestBooking>().HasData(
                new GuestBooking { EventId = 1, GuestId = 1 },
                new GuestBooking { EventId = 1, GuestId = 2},
                new GuestBooking { EventId = 2, GuestId = 3}
            );

            // Seed data for Staff
            builder.Entity<Staff>().HasData(
                new Staff { StaffId = 1, StaffName = "Alice Brown", Email = "alice.brown@company.com" },
                new Staff { StaffId = 2, StaffName = "Bob White", Email = "bob.white@company.com" }
            );

            builder.Entity<Staffing>().HasData(
           new Staffing { StaffRole = "Event Manager", EventId = 1}, // Event Manager for Tech Conference
           new Staffing { StaffRole = "Security", EventId = 1 }, // Security for Tech Conference
           new Staffing { StaffRole = "Curator", EventId = 1 }, // Curator for Art Exhibition
           new Staffing { StaffRole = "Security", EventId = 2}  // Security for Art Exhibition
       );

        }


    }
}