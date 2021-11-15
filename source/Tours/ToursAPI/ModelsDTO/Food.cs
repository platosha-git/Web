using System;
using ToursWeb.ModelsDB;

namespace ToursAPI.ModelsDTO
{
    public class FoodDTO
    {
        public int Foodid { get; set; }
        public string Category { get; set; }
        public bool? Vegmenu { get; set; }
        public bool? Childrenmenu { get; set; }
        public bool? Bar { get; set; }
        public int Cost { get; set; }

        public FoodDTO()
        {
        }

        public FoodDTO(Food food)
        {
            Foodid = food.Foodid;
            Category = food.Category;
            Vegmenu = food.Vegmenu;
            Childrenmenu = food.Childrenmenu;
            Bar = food.Bar;
            Cost = food.Cost;
        }

        public Food GetFood()
        {
            Food food = new Food ()
            {
                Foodid = Foodid,
                Category = Category,
                Vegmenu = Vegmenu,
                Childrenmenu = Childrenmenu,
                Bar = Bar,
                Cost = Cost
            };
            
            return food;
        }

        public bool AreEqual(Food food)
        {
            if (Foodid == food.Foodid &&
                Category == food.Category &&
                Vegmenu == food.Vegmenu &&
                Childrenmenu == food.Childrenmenu &&
                Bar == food.Bar &&
                Cost == food.Cost)
                return true;
            return false;
        }
    }
}