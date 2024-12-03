using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Pages.ViewModels
{
    public class GuestVM
    {

        public int GuestId { get; set; }
        // Property to hold the guest's name
        public string GuestName { get; set; }

        // Property to hold the guest's contact number as a string
        // to support phone number formats that include dashes, parentheses, etc.
      
        public  int GuestContact { get; set; }

        // You could add validation or additional logic, such as:
        public bool IsValidContact => GuestContact.ToString().Length == 11; // Example validation
    }

    //public class EditGuestVM : GuestVM
    //{
    //    public int GuestId { get; set; }
    //}



}
