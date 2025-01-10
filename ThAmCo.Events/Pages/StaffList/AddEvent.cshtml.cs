using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Pages.ViewModels;

namespace ThAmCo.Events.Pages.StaffList
{
    public class AddEventModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public AddEventModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Staffing Staffing { get; set; } = default!;
        public SelectList Staff{ get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffing =  await _context.staffings.FirstOrDefaultAsync(m => m.StaffRole == id);
            if (staffing == null)
            {
                return NotFound();
            }
            Staffing = staffing;
           
            Staff = new SelectList(await _context.Guests.ToListAsync(), "EventId", "StaffId");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Staffing.StaffId", "Please select a staff.");
                Staff = new SelectList(await _context.Guests.ToListAsync(), "StaffId", "EventId");
                return Page();
            }

            _context.Attach(Staffing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffingExists(Staffing.StaffRole))
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

        private bool StaffingExists(string id)
        {
            return _context.staffings.Any(e => e.StaffRole == id);
        }
    }
}
