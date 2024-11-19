using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.DTO
{
    public class PostFoodBookingDTO
    {
        public int FoodBookingId { get; set; }
        public int ClientReferenceId { get; set; }
        public int NumberOfGuests { get; set; }
        public int MenuId { get; set; }

        public  static FoodBooking BuildFromDTO(PostFoodBookingDTO DTO)
        {
            return new FoodBooking()
            {
                 FoodBookingId = DTO.FoodBookingId,
                  ClientReferenceId = DTO.ClientReferenceId,
                   MenuId = DTO.MenuId,
                    NumberOfGuests = DTO.NumberOfGuests,
            };
        }
    }
}
