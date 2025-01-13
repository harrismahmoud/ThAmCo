using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Pages.ViewModels;

namespace ThAmCo.Events.Pages.EventList
{
    public class AddGuestModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public AddGuestModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GuestBooking GuestBooking { get; set; } = default!;
        // This is for the dropdown to be populated
        public SelectList Guests { get; set; }

        // List of guests attending the event
        public List<Guest> AttendingGuests { get; set; }

        // List of all events a specific guest is associated with
        public List<GuestEventDetails> GuestEvents { get; set; }

        // Define the GuestId property to bind the drop-down
        [BindProperty(SupportsGet = true)]
        public int? EventId { get; set; }  // Nullable to handle "no selection"

        public async Task<IActionResult> OnGetAsync(int? EventId)
        {
          
            if (EventId == null)
            {
                return NotFound();
            }

        

            var guestbooking =  await _context.guestBookings.FirstOrDefaultAsync(m => m.GuestId == EventId);
            if (guestbooking == null)
            {
                return NotFound();
            }
            GuestBooking = guestbooking;


            // Populate the dropdown with all available guests
            Guests = new SelectList(await _context.Guests.ToListAsync(), "GuestId", "GuestName");


            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If the model state is not valid, return to the page
                Guests = new SelectList(await _context.Guests.ToListAsync(), "GuestId", "GuestName");
                return Page();
            }

            if (GuestBooking.GuestId == 0)
            {
                ModelState.AddModelError("GuestBooking.GuestId", "Please select a guest.");
                Guests = new SelectList(await _context.Guests.ToListAsync(), "GuestId", "GuestName");
                return Page();
            }

            // Check if the GuestBooking already exists for this event
            var existingGuestBooking = await _context.guestBookings
                .FirstOrDefaultAsync(gb => gb.EventId == GuestBooking.EventId && gb.GuestId == GuestBooking.GuestId);

            if (existingGuestBooking != null)
            {
                // Optionally, handle this case where the guest is already booked for the event
                ModelState.AddModelError(string.Empty, "This guest is already added to the event.");
                Guests = new SelectList(await _context.Guests.ToListAsync(), "GuestId", "GuestName");
                return Page();
            }

            // Add a new guest booking record
            var newGuestBooking = new GuestBooking
            {
                EventId = GuestBooking.EventId,
                GuestId = GuestBooking.GuestId
            };

            if (EventId.HasValue)
            {
                // Example: update the GuestBooking or other operations
                GuestBooking.GuestId = EventId.Value; // Set the selected GuestId
                _context.Attach(GuestBooking).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestBookingExists(GuestBooking.EventId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }                   

        private bool GuestBookingExists(int? id)
        {
            return _context.guestBookings.Any(e => e.EventId == id);
        }

        // DTO class for displaying guest event details
        public class GuestEventDetails
        {
            public string EventName { get; set; }
            public DateTime EventDate { get; set; }
        }
    }
}

    