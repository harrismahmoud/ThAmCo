namespace ThAmCo.Events.Data
{
    public class Staff
    {
        public int StaffId { get; set; }    
        public string StaffName { get; set; }
        public string Email { get; set; }

        public List<Staffing> staffing { get; set; }


    }
}
