using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Pages.StaffList
{
    public class DetailsModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public DetailsModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        public Staff Staff { get; set; } = default!;

        public List<Staff> AssignedStaff { get; set; } = new List<Staff>();
        public async Task<IActionResult> OnGetAsync(int? StaffId)
        {
            if (StaffId == null)
            {
                return NotFound();
            }

            var staff = await _context.staffs.FirstOrDefaultAsync(m => m.StaffId == StaffId);
            if (staff == null)
            {
                return NotFound();
            }
            else
            {
                Staff = staff;
            }

            // Retrieve the staff members assigned to the event
            AssignedStaff = await _context.staffings
                .Where(s => s.EventId == StaffId)
                .Include(s => s.Staff)  // Include the related Staff
                .Select(s => s.Staff)  // Select the actual Staff entity
                .ToListAsync();


            return Page();
        }
    }
}
