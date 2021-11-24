using ToursWeb.ModelsDB; 

namespace ToursWeb.ModelsBL
{
    public enum FMenu
    {
        Vegeterian,
        Children,
        Dietary
    };
    
    public class FoodBL
    {
        public int Foodid { get; set; }
        public string Category { get; set; }
        public string Menu { get; set; }
        public bool? Bar { get; set; }
        public int Cost { get; set; }

        public FoodBL() { }

        public FoodBL(Food food)
        {
            Foodid = food.Foodid;
            Category = food.Category;
            Menu = food.Menu;
            Bar = food.Bar;
            Cost = food.Cost;
        }
        
        public Food GetFood()
        {
            Food food = new Food ()
            {
                Foodid = Foodid,
                Category = Category,
                Menu = Menu,
                Bar = Bar,
                Cost = Cost
            };
            
            return food;
        }
    }
}