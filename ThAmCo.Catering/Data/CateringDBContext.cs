using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace ThAmCo.Catering.Data
{
    public class CateringDBContext : DbContext
    {
        // defining the database class for catering

        public DbSet<FoodBooking> FoodBookings { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Menu> menus { get; set; }
        public DbSet<MenuFoodItem> MenuItems { get; set; }
        private string DbPath { get; set; } = string.Empty;

        //Constructor to setup the DB path and name
        public CateringDBContext() 
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "Catering.db");
        }
        //Defining the keys for the db
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FoodBooking>()
                .HasKey(fb => new { fb.FoodBookingId});

            builder.Entity<FoodItem>()
                .HasKey(fi => fi.FoodItemId);

            builder.Entity<Menu>()
                .HasKey(mi => mi.MenuId);

            builder.Entity<MenuFoodItem>()
                .HasKey(mfi => new { mfi.MenuId, mfi.FoodItems});


            //Seed Data for the DB
            builder.Entity<FoodItem>().HasData(
           new FoodItem { FoodItemId = 1, Name = "Pizza", Description = "Cheese and tomato pizza", Price = 12.99 },
           new FoodItem { FoodItemId = 2, Name = "Pasta", Description = "Spaghetti with marinara sauce", Price = 9.99 },
           new FoodItem { FoodItemId = 3, Name = "Salad", Description = "Mixed greens with balsamic dressing", Price = 5.99 },
           new FoodItem { FoodItemId = 4, Name = "Burger", Description = "Beef burger with lettuce and tomato", Price = 8.99 },
           new FoodItem { FoodItemId = 5, Name = "Soda", Description = "Carbonated drink", Price = 1.99}
       );

            // Seed Menus
            builder.Entity<Menu>().HasData(
                new Menu { MenuId = 1, MenuName = "Vegetarian Delight"},
                new Menu { MenuId = 2, MenuName = "Italian Feast" },
                new Menu { MenuId = 3, MenuName = "Classic Burger Meal"}
            );

            // Seed MenuFoodItems (linking FoodItems to Menus)
            builder.Entity<MenuFoodItem>().HasData(
                new MenuFoodItem { MenuId = 1, FoodMenuId = 3 }, // Vegetarian Delight includes Salad
                new MenuFoodItem { MenuId = 1, FoodMenuId = 4 }, // Vegetarian Delight includes Burger
                new MenuFoodItem { MenuId = 2, FoodMenuId = 1 }, // Italian Feast includes Pizza
                new MenuFoodItem { MenuId = 2, FoodMenuId = 2 }, // Italian Feast includes Pasta
                new MenuFoodItem { MenuId = 2, FoodMenuId = 5 }, // Italian Feast includes Soda
                new MenuFoodItem { MenuId = 3, FoodMenuId = 4 }, // Classic Burger Meal includes Burger
                new MenuFoodItem { MenuId = 3, FoodMenuId = 5 }  // Classic Burger Meal includes Soda
            );

            // Seed FoodBookings
            builder.Entity<FoodBooking>().HasData(
                 new FoodBooking{ FoodBookingId = 1,ClientReferenceId = 101, NumberOfGuests = 10, MenuId = 1},
                 new FoodBooking { FoodBookingId = 2, ClientReferenceId = 102, NumberOfGuests = 15, MenuId = 2 },
                 new FoodBooking { FoodBookingId = 3, ClientReferenceId = 103, NumberOfGuests = 20, MenuId = 3 },
                 new FoodBooking { FoodBookingId = 4, ClientReferenceId = 104, NumberOfGuests = 5, MenuId = 1 },
                 new FoodBooking { FoodBookingId = 5, ClientReferenceId = 105, NumberOfGuests = 8, MenuId = 2 }
            );
        }

    }
 }

