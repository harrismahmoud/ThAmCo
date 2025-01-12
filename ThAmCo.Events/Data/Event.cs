namespace ThAmCo.Events.Data
{
    public class Event
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventType { get; set; }
        public int ReservationId { get; set; }
        public int FoodBookingId { get; set; }

       


        public ICollection<GuestBooking> GuestBooking { get; set; }
        public ICollection<Staffing> staffing { get; set; }

    }
}
