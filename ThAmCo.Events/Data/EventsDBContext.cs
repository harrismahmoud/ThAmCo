using Microsoft.EntityFrameworkCore;

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
                .WithOne(e => e.EventId)
                .HasForeignKey(e => e.EventId);

            builder.Entity<Staff>()
                .HasMany(st=> st.staffing)
                .WithOne(st=> st.Staff)
                .HasForeignKey(s => s.EventId);


            builder.Entity<Event>()
                .HasMany(gb => gb.GuestBooking)
                .WithOne(gb => gb.Event);

            builder.Entity<Guest>()
                .HasMany(gb => gb.GuestBooking)
                .WithOne(g => g.Guest);
                

        }


    }
}