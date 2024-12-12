using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.GBList
{
    public class DetailsModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public DetailsModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        public GuestBooking GuestBooking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestbooking = await _context.guestBookings.FirstOrDefaultAsync(m => m.EventId == id);
            if (guestbooking == null)
            {
                return NotFound();
            }
            else
            {
                GuestBooking = guestbooking;
            }
            return Page();
        }
    }
}
