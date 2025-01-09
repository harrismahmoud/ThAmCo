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

        public async Task<IActionResult> OnGetAsync(int? EventId)
        {
          
            if (EventId == null)
            {
                return NotFound();
            }

            var guestbooking =  await _context.guestBookings.FirstOrDefaultAsync(m => m.EventId == EventId);
            if (guestbooking == null)
            {
                return NotFound();
            }
            GuestBooking = guestbooking;

            // Populate the dropdown with all available guests
            Guests = new SelectList(await _context.Guests.ToListAsync(), "GuestId", "GuestName");

            // Fetch the list of guests who are attending this event
            AttendingGuests = await _context.guestBookings
                .Where(gb => gb.EventId == EventId)
                .Include(gb => gb.Guest)  // Ensure that Guest is included in the result
                .Select(gb => gb.Guest)   // Select the actual Guest entity
                .ToListAsync();

            // If AttendingGuests is null, initialize it as an empty list
            if (AttendingGuests == null)
            {
                AttendingGuests = new List<Guest>();
            }

            // Get all events for a specific guest
            GuestEvents = await _context.guestBookings
                .Where(gb => gb.GuestId == GuestBooking.GuestId)
                .Include(gb => gb.Event) // Include associated events
                .Select(gb => new GuestEventDetails
                {
                    EventName = gb.Event.EventName,
                    EventDate = gb.Event.EventDateTime
                }).ToListAsync();

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

            // Add the new booking to the context
            _context.guestBookings.Add(newGuestBooking);

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

        private bool GuestBookingExists(int id)
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

    