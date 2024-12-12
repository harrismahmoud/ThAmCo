using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThAmCo.Events.Data;
using ThAmCo.Events.Pages.ViewModels;

namespace ThAmCo.Events.Pages.GBList
{
    public class CreateModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public GBVM vm { get; set; } = new GBVM();
        public CreateModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int EventId)
        {
            vm.EventId = EventId;
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "EventId");
        ViewData["GuestId"] = new SelectList(_context.Guests, "GuestId", "GuestId");
            return Page();
        }

        [BindProperty]
        public GuestBooking GuestBooking { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            //GuestBooking = new GuestBooking { EventId = 1, GuestId = 1 };
            GBVM vm = new GBVM();

            _context.guestBookings.Add(GuestBooking);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
