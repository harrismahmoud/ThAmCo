﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.GBList
{
    public class DeleteModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public DeleteModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GuestBooking GuestBooking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? EventId)
        {
            if (EventId == null)
            {
                return NotFound();
            }

            var guestbooking = await _context.guestBookings.FirstOrDefaultAsync(m => m.EventId == EventId);

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

        public async Task<IActionResult> OnPostAsync(int? EventId)
        {
            if (EventId == null)
            {
                return NotFound();
            }

            var guestbooking = await _context.guestBookings.FindAsync(EventId);
            if (guestbooking != null)
            {
                GuestBooking = guestbooking;
                _context.guestBookings.Remove(GuestBooking);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
