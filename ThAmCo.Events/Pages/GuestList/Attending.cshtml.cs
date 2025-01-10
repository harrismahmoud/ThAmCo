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

        public IList<Guest> Guest { get; set; }
        public SelectList GuestsList { get; set; }

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

            Guest = await _context.Guests.ToListAsync();
            GuestsList = new SelectList(_context.Guests, "GuestId", "GuestName");

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

            // Use the selected GuestId for your logic
            if (GuestId.HasValue)
            {
                // Example: update the GuestBooking or other operations
                GuestBooking.GuestId = GuestId.Value;
                 // Set the selected GuestId
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

        private bool GuestBookingExists(int GuestId)
        {
            return _context.guestBookings.Any(e => e.EventId == GuestId);
        }
    }
}
