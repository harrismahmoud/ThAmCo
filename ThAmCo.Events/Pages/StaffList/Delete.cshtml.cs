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
    public class DeleteModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public DeleteModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Staff Staff { get; set; } = default!;

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? StaffId)
        {
            if (StaffId == null)
            {
                return NotFound();
            }

            var staff = await _context.staffs.FindAsync(StaffId);
            if (staff != null)
            {
                Staff = staff;
                _context.staffs.Remove(Staff);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
