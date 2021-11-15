using System;
using System.Collections.Generic;
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

        public List<Food> GetFoodByVegMenu(bool vm)
        {
            return _foodRepository.FindFoodByVegMenu(vm);
        }

        public List<Food> GetFoodByChildMenu(bool cm)
        {
            return _foodRepository.FindFoodByChildMenu(cm);
        }

        public List<Food> GetFoodByBar(bool bar)
        {
            return _foodRepository.FindFoodByBar(bar);
        }
        
        public void AddFood(Food nfood)
        {
            _foodRepository.Add(nfood);
        }
        
        public void UpdateFood(Food nfood)
        {
            _foodRepository.Update(nfood);
        }
        
        public void DeleteFoodByID(int foodID)
        {
            _foodRepository.DeleteByID(foodID);
        }
    }
}