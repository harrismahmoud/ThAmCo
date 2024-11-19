using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.DTO
{
    public class PostFoodItemsDTO
    {
        public int FoodItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public static FoodItem BuildFromDTO(PostFoodItemsDTO DTO)
        {
            return new FoodItem()
            {
                FoodItemId = DTO.FoodItemId,
                 Name = DTO.Name,
                  Description = DTO.Description,
                   Price = DTO.Price
            };
        }

       
    }
}
