using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using static ThAmCo.Events.Pages.EventList.AddGuestModel;

namespace ThAmCo.Events.Pages.GuestList
{
    public class DetailsModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public DetailsModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GuestBooking GuestBooking { get; set; } = default!;

        public Guest Guest { get; set; } = default!;
      
        public List<GuestEventDetails> GuestEvents { get; set; }





        public async Task<IActionResult> OnGetAsync(int GuestId, int EventId)
        {
            if (GuestId == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests.FirstOrDefaultAsync(m => m.GuestId == GuestId);
            if (guest == null)
            {
                return NotFound();
            }
            else
            {
                Guest = guest;
                
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

        public class GuestEventDetails
        {
            public string EventName { get; set; }
            public DateTime EventDate { get; set; }
        }
    }
}
