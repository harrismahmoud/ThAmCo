namespace ThAmCo.Events.Data
{
    public class GuestBooking
    {

        public int GuestBookingId { get; set; }

        public int PartyNumber { get; set; }

        public DateTime BookingDate { get; set; }

        public Event Event { get; set; }
    }
}
