namespace ThAmCo.Events.Pages.ViewModels
{
    public class EventVM
    { 
            public int EventId { get; set; }
            // The name of the event
            public string EventName { get; set; }

            // The date and time of the event
            public DateTime EventDateTime { get; set; }

            // The type of event (e.g., Wedding, Conference, etc.)
            public string EventType { get; set; }

            // The ID of the reservation related to the event
            public int ReservationId { get; set; }

            // The ID of the food booking related to the event
            public int FoodBookingId { get; set; }

        

            // Optionally, you can add validation or formatting logic for presentation
            public string FormattedEventDateTime => EventDateTime.ToString("f"); // Example of formatted DateTime for the view
        }

    }

