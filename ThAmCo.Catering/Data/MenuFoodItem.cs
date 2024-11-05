namespace ThAmCo.Catering.Data
{
    public class MenuFoodItem
    {
        public int MenuId { get; set; }
        public int FoodMenuId { get; set; }

        public ICollection<Menu> Menus { get; set; }

        public ICollection<FoodItem> FoodItems { get; set; }
    }
}
