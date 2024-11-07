namespace ThAmCo.Catering.Data
{
    public class FoodItem
    {
       public  int FoodItemId { get; set; }
       public  string Name  { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public ICollection<MenuFoodItem> MenuFoodItems { get; set; }



    }
}
