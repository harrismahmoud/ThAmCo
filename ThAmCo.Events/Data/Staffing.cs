namespace ThAmCo.Events.Data
{
    public class Staffing
    {
        public int EventId { get; set; }
        public string StaffRole { get; set; }

        public Event Event { get; set; }

        public Staff Staff { get; set; }


    }
}
