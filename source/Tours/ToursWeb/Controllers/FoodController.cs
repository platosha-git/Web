using System.Collections.Generic;
using System.Linq;
using ToursWeb.ModelsDB;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class FoodController
    {
        private readonly IFoodRepository _foodRepository;

        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        
        public List<Food> GetAllFood()
        {
            return _foodRepository.FindAll();
        }

        public Food GetFoodByID(int foodID)
        {
            return _foodRepository.FindByID(foodID);
        }

        public List<Food> GetFoodByCategory(string cat)
        {
            return _foodRepository.FindFoodByCategory(cat);
        }

        public List<Food> GetFoodByMenu(string menu)
        {
            return _foodRepository.FindFoodByMenu(menu);
        }

        public List<Food> GetFoodByBar(bool bar)
        {
            return _foodRepository.FindFoodByBar(bar);
        }
        
        public ExitCode AddFood(Food nfood)
        {
            return _foodRepository.Add(nfood);
        }
        
        public ExitCode UpdateFood(Food nfood)
        {
            return _foodRepository.Update(nfood);
        }
        
        public ExitCode DeleteFoodByID(int foodID)
        {
            return _foodRepository.DeleteByID(foodID);
        }
    }
}