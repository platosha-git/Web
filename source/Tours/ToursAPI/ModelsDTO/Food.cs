using ToursWeb.ModelsDB;

namespace ToursAPI.ModelsDTO
{
    public class FoodUserDTO 
    {
        public string Category { get; set; }
        public string Menu { get; set; }
        public bool? Bar { get; set; }
        public int Cost { get; set; }

        public FoodUserDTO()
        {
        }
        
        public Food GetFood(int foodID = 0)
        {
            Food food = new Food ()
            {
                Foodid = foodID,
                Category = Category,
                Menu = Menu,
                Bar = Bar,
                Cost = Cost
            };
            
            return food;
        }
    }
    
    public class FoodDTO : FoodUserDTO
    {
        public int Foodid { get; set; }

        public FoodDTO() {}
        
        public FoodDTO(Food food)
        {
            Foodid = food.Foodid;
            Category = food.Category;
            Menu = food.Menu;
            Bar = food.Bar;
            Cost = food.Cost;
        }

        public bool AreEqual(Food food)
        {
            if (Foodid == food.Foodid &&
                Category == food.Category &&
                Menu == food.Menu &&
                Bar == food.Bar &&
                Cost == food.Cost)
                return true;
            return false;
        }
    }
}