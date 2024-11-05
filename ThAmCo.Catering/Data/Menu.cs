namespace ThAmCo.Catering.Data
{
    public class Menu
    {
       public int MenuId { get; set; }
        public string MenuName { get; set; }

        public ICollection<MenuFoodItem> Items { get; set; }
        public ICollection<FoodItem> FoodItems { get; set; }


    }
}
