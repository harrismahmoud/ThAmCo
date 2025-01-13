using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace ThAmCo.Events.Data
{
    public class GuestBooking
    {

        public int? GuestId { get; set; }

        public int? EventId { get; set; }

        [ValidateNever]
        public Guest Guest { get; set; }

        [ValidateNever]
        public Event Event { get; set; }
        
    }
}
