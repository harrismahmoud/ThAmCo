using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.GuestList
{
    public class AttendingModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public AttendingModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GuestBooking GuestBooking { get; set; }

        public List<GuestEventDetails> GuestEvents { get; set; }
        public List<Guest> AttendingGuests { get; set; }

        // Define the GuestId property to bind the drop-down
        [BindProperty(SupportsGet = true)]
        public int? EventId { get; set; }  // Nullable to handle "no selection"

     

        public async Task<IActionResult> OnGetAsync(int? GuestId )
        {
            if (GuestId == null)
            {
                return NotFound();
            }

            var guestbooking =  await _context.guestBookings.FirstOrDefaultAsync(m => m.EventId == GuestId);
            if (guestbooking == null)
            {
                return NotFound();
            }
            GuestBooking = guestbooking;

            

            // Fetch the list of guests who are attending this event
            
              AttendingGuests = await _context.guestBookings
                .Where(gb => gb.EventId == GuestId)
                .Include(gb => gb.Guest)  // Ensure that Guest is included in the result
                .Select(gb => gb.Guest)   // Select the actual Guest entity
                .ToListAsync();

            // If AttendingGuests is null, initialize it as an empty list
            if (AttendingGuests == null)
            {
                AttendingGuests = new List<Guest>();
            }

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
            // Ensure that the EventId property is set before attempting to modify the entity
            if (GuestBooking.EventId == 0)  // Assuming EventId is a non-nullable integer
            {
                ModelState.AddModelError("GuestBooking.EventId", "EventId cannot be zero.");
               

            }

            //Get all events for a specific guest

            //GuestEvents = await _context.guestBookings
            //   .Where(gb => gb.GuestId == GuestBooking.GuestId)
            //   .Include(gb => gb.Event) // Include associated events
            //   .Select(gb => new GuestEventDetails
            //   {
            //       EventName = gb.Event.EventName,
            //       EventDate = gb.Event.EventDateTime
            //   }).ToListAsync();

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
                if (!GuestBookingExists(GuestBooking.GuestId))
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

        private bool GuestBookingExists(int? GuestId)
        {
            return _context.guestBookings.Any(e => e.EventId == GuestId);
        }

        public class GuestEventDetails
        {
            public string EventName { get; set; }
            public DateTime EventDate { get; set; }
        }
    }
}
