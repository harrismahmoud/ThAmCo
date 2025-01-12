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
        public GuestBooking GuestBooking { get; set; } = default!;

        public List<GuestEventDetails> GuestEvents { get; set; }

        // Define the GuestId property to bind the drop-down
        [BindProperty(SupportsGet = true)]
        public int? GuestId { get; set; }  // Nullable to handle "no selection"

     

        public async Task<IActionResult> OnGetAsync(int? GuestId)
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

           GuestEvents = await _context.guestBookings
               .Where(gb => gb.GuestId == GuestBooking.GuestId)
               .Include(gb => gb.Event) // Include associated events
               .Select(gb => new GuestEventDetails
               {
                   EventName = gb.Event.EventName,
                   EventDate = gb.Event.EventDateTime
               }).ToListAsync();


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

        private bool GuestBookingExists(int GuestId)
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
