namespace ThAmCo.Events.Data
{
    public class GuestBooking
    {

        public int GuestId { get; set; }

        public int EventId { get; set; }

        public Guest Guest { get; set; }

        public Event Event { get; set; }
        
    }
}
