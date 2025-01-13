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
        public Staff Staff { get; set; } = default!;
        public SelectList StaffList { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? EventId { get; set; }  // Nullable to handle "no selection"

        public async Task<IActionResult> OnGetAsync(int? EventId)
        {
            if (EventId == null)
            {
                return NotFound();
            }

            var staff =  await _context.staffs.FirstOrDefaultAsync(m => m.StaffId == EventId);
            if (staff != null)
            {
                return NotFound();
            }
            Staff = staff;

            StaffList = new SelectList(await _context.staffs.ToListAsync(), "StaffId", "StaffName");



            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                StaffList = new SelectList(await _context.staffs.ToListAsync(), "StaffId", "StaffName");
                return Page();
            }

            if (Staff.StaffId == 0)
            {
                ModelState.AddModelError("Staff.StaffId", "Please select Staff.");
                StaffList = new SelectList(await _context.staffs.ToListAsync(), "StaffId", "StaffName");
                return Page();
            }

            // Check if the GuestBooking already exists for this event
            var existingStaff = await _context.staffs
                .FirstOrDefaultAsync(gb => gb.StaffId == Staff.StaffId && gb.StaffId == Staff.StaffId);



            // Add a new guest booking record
            var newStaff = new Staff
            {
                StaffId = Staff.StaffId,
                StaffName = Staff.StaffName
            };

            if (EventId.HasValue)
            {
                // Example: update the GuestBooking or other operations
                Staff.StaffId = EventId.Value; // Set the selected GuestId
                //_context.Attach(Staff).State = EntityState.Modified;
            }

            

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(Staff.StaffId))
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

        private bool StaffExists(int id)
        {
            return _context.staffs.Any(e => e.StaffId == id);
        }
    }
}
