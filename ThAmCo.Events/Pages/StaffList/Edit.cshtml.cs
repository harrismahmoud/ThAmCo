using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ThAmCo.Events.Data;
using ThAmCo.Events.Pages.ViewModels;

namespace ThAmCo.Events.Pages.StaffList
{
    public class EditModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        [BindProperty]
        public StaffVM vm { get; set; } = new StaffVM();

        public EditModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        
        public Staff Staff { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int StaffId, string StaffName, string Email )
        {
            

            

            var staff =  await _context.staffs.FirstOrDefaultAsync(m => m.StaffId == StaffId);
            if (staff == null)
            {
                return NotFound();
            }


            vm.StaffId = StaffId;
            vm.StaffName = StaffName;
            vm.Email = Email;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _context.Attach(Staff).State = EntityState.Modified;

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

            return Page();
        }

        private bool StaffExists(int StaffId)
        {
            return _context.staffs.Any(e => e.StaffId == StaffId);
        }
    }
}
