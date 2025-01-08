using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using static ThAmCo.Events.Pages.EventList.AddGuestModel;

namespace ThAmCo.Events.Pages.GuestList
{
    public class DetailsModel : PageModel
    {
        private readonly ThAmCo.Events.Data.EventsDBContext _context;

        public DetailsModel(ThAmCo.Events.Data.EventsDBContext context)
        {
            _context = context;
        }

        public Guest Guest { get; set; } = default!;
     




        public async Task<IActionResult> OnGetAsync(int? GuestId)
        {
            if (GuestId == null)
            {
                return NotFound();
            }

            var guest = await _context.Guests.FirstOrDefaultAsync(m => m.GuestId == GuestId);
            if (guest == null)
            {
                return NotFound();
            }
            else
            {
                Guest = guest;
                
            }


    

            return Page();
        }
    }
}
