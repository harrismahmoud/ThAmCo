namespace ThAmCo.Catering.Data
{
    public class Menu
    {
        /// <summary>
        /// Fred
        /// </summary>
       public int MenuId { get; set; }
        public string MenuName { get; set; }

        public ICollection<MenuFoodItem> Item { get; set; }


       


    }
}
