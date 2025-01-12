using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Pages.ViewModels;

namespace ThAmCo.Events.Pages.EventList
{
    public class AddStaffModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public AddStaffModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Staffing Staffing { get; set; } = default!;

        public List<Staff> Staff { get; set; }
        public SelectList StaffList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? EventId { get; set; }  // Nullable to handle "no selection"

        public async Task<IActionResult> OnGetAsync(string EventId)
        {
            if (EventId == null)
            {
                return NotFound();
            }

            var staffing =  await _context.staffings.FirstOrDefaultAsync(m => m.StaffRole == EventId);
            if (staffing == null)
            {
                return NotFound();
            }
            Staffing = staffing;

            Staff = await _context.staffs.ToListAsync();
            StaffList = new SelectList(_context.staffs, "StaffId", "StaffName");



            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (EventId.HasValue)
            {
                // Example: update the GuestBooking or other operations
                Staffing.EventId = EventId.Value;
                // Set the selected GuestId
                _context.Attach(Staffing).State = EntityState.Modified;
            }

            

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
