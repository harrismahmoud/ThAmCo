using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.DTO
{
    public class PostMenuFoodItemsDTO
    {
        public int MenuId { get; set; }
        public int FoodMenuId { get; set; }

        public static MenuFoodItem BuildFromDTO(MenuFoodItem DTO)
        {
            return new MenuFoodItem()
            {
                MenuId = DTO.MenuId,
                FoodMenuId = DTO.FoodMenuId
            };
        }

        internal static MenuFoodItem BuildFromDTO(PostMenuFoodItemsDTO menuFoodItem)
        {
            throw new NotImplementedException();
        }
    }
}
