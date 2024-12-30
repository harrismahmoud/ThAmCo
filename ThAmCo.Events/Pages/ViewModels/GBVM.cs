using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Pages.ViewModels
{
    public class GBVM
    {
        public int EventId { get; set; }
        public int GuestId { get; set; }

        public Boolean attended { get; set; }
       
       

        
    }
}
