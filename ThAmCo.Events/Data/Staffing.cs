using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ThAmCo.Events.Data
{
    public class Staffing
    {
        public int EventId { get; set; }
        public string StaffRole { get; set; }

        [ValidateNever]
        public Event Event { get; set; }
        [ValidateNever]
        public Staff Staff { get; set; }


    }
}
