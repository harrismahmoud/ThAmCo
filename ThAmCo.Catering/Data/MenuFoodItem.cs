namespace ThAmCo.Catering.Data
{
    public class MenuFoodItem
    {
        public int MenuId { get; set; }

        public int FoodMenuId { get; set; }

        public Menu Menus { get; set; }

        public FoodItem FoodItems { get; set; }

        
    }
}
