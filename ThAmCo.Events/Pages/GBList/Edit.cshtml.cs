using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.GBList
{
    public class EditModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public EditModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GuestBooking GuestBooking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guestbooking =  await _context.guestBookings.FirstOrDefaultAsync(m => m.EventId == id);
            if (guestbooking == null)
            {
                return NotFound();
            }
            GuestBooking = guestbooking;
           ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId");
           ViewData["EventId"] = new SelectList(_context.Guests, "GuestId", "GuestId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(GuestBooking).State = EntityState.Modified;

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
    }
}
