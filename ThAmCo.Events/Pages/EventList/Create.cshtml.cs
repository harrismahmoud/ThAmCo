﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ThAmCo.Events.Data;
using ThAmCo.Events.Pages.ViewModels;

namespace ThAmCo.Events.Pages.EventList
{
    public class CreateModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        [BindProperty]
        public EventVM vm { get; set; } = new EventVM();

        public CreateModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int EventId)
        {
            vm.EventId = EventId;
            return Page();
        }

        [BindProperty]
        public Event Event { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            EventVM vm = new EventVM();

            _context.Events.Add(Event);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
