﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.EventList
{
    public class DeleteModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public DeleteModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? EventId)
        {
            if (EventId == null)
            {
                return NotFound();
            }

            var Events = await _context.Events.FirstOrDefaultAsync(m => m.EventId == EventId);

            if (Events == null)
            {
                return NotFound();
            }
            else
            {
                Event = Events;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? EventId)
        {
            if (EventId == null)
            {
                return NotFound();
            }

            var Events = await _context.Events.FindAsync(EventId);
            if (Events != null)
            {
                Event = Events;
                _context.Events.Remove(Event);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
