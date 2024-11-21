namespace ThAmCo.Events.Data
{
    public class Guest
    {
        public int GuestId {  get; set; }
        public string GuestName { get; set; }
        public int GuestContact {  get; set; }


        public ICollection<GuestBooking> GuestBooking { get; set; }


    }
}
