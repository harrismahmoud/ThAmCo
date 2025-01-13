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
        public Staff Staff { get; set; } = default!;
        public SelectList StaffList{ get; set; }

        public async Task<IActionResult> OnGetAsync(string EventId)
        {
            if (EventId == null)
            {
                return NotFound();
            }

            var staffing =  await _context.staffs.FirstOrDefaultAsync(m => m.StaffName == EventId);
            if (staffing == null)
            {
                return NotFound();
            }
            Staff = Staff;
           
            StaffList = new SelectList(await _context.staffs.ToListAsync(), "EventId", "StaffId");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Staffing.StaffId", "Please select a staff.");
                StaffList = new SelectList(await _context.Guests.ToListAsync(), "StaffId", "EventId");
                return Page();
            }

            _context.Attach(Staff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffingExists(Staff.StaffName))
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
            return _context.staffs.Any(e => e.StaffName == id);
        }
    }
}
