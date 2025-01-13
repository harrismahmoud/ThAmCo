using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Pages.ViewModels;

namespace ThAmCo.Events.Pages.EventList
{
    public class DetailsModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public DetailsModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        public GuestBooking GuestBooking{get; set;}
  
       

        public Event Event { get; set; } = default!;

        public List<Guest> AttendingGuests { get; set; }

        public List<Staff> AssignedStaff { get; set; } = new List<Staff>();


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

            AttendingGuests = await _context.guestBookings
               .Where(gb => gb.EventId == EventId)
               .Include(gb => gb.Guest)  // Ensure that Guest is included in the result
               .Select(gb => gb.Guest)   // Select the actual Guest entity
               .ToListAsync();

            // If AttendingGuests is null, initialize it as an empty list
            if (AttendingGuests == null)
            {
                AttendingGuests = new List<Guest>();
            }

            // Retrieve the staff members assigned to the event
            AssignedStaff = await _context.staffings
                .Where(s => s.EventId == EventId)
                .Include(s => s.Staff)  // Include the related Staff
                .Select(s => s.Staff)  // Select the actual Staff entity
                .ToListAsync();





            return Page();
        }

        public async Task<IActionResult> OnPostRemoveStaffAsync(int StaffId)
        {
            // Check if the staff member exists for this event
            var staffing = await _context.staffs
                .FirstOrDefaultAsync(s => s.StaffId == StaffId);

            if (staffing != null)
            {
                // Remove the staff assignment from the event
                _context.staffs.Remove(staffing);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }



    }

 }

