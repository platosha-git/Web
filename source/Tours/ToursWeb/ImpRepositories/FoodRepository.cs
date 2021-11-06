using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class FoodRepository : IFoodRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<FoodRepository> _logger;

        public FoodRepository(ToursContext createDB, ILogger<FoodRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        public List<Food> FindAll()
        {
            return _db.Foods.ToList();
        }

        public Food FindByID(int id)
        {
            return _db.Foods.Find(id);
        }

        public void Add(Food obj)
        {
            try 
            {
                obj.Foodid = _db.Foods.Count() + 1;
                _db.Foods.Add(obj);
                _db.SaveChanges();
                _logger.LogInformation("+FoodRep : Food {Number} was added to Food", obj.Foodid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+FoodRep : Error trying to add food to Food");
            }
        }

        public void Update(Food obj)
        {
            try
            {
                Food uFood = FindByID(obj.Foodid);
                uFood.Category = obj.Category; uFood.Vegmenu = obj.Vegmenu; 
                uFood.Childrenmenu = obj.Childrenmenu; uFood.Bar = obj.Bar; 
                uFood.Cost = obj.Cost;

                _db.Foods.Update(uFood);
                _db.SaveChanges();
                _logger.LogInformation("+FoodRep : Food {Number} was updated at Food", obj.Foodid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+FoodRep : Error trying to update food to Food");
            }
        }

        public void DeleteAll()
        {
            try
            {
                List<Food> allFoods = FindAll();
                _db.Foods.RemoveRange(allFoods);
                _db.SaveChanges();
                _logger.LogInformation("+FoodRep : All food were deleted from Food");
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+FoodRep : Error trying to delete all food from Food");
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Food food = FindByID(id);
                _db.Foods.Remove(food);
                _db.SaveChanges();
                _logger.LogInformation("+FoodRep : Food {Number} was deleted from Food", id);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+FoodRep : Error trying to delete food {Number} from Food", id);
            }
        }

        public List<Food> FindFoodByCategory(string cat)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Category == cat);
            return foods.ToList();
        }

        public List<Food> FindFoodByVegMenu(bool vm)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Vegmenu == vm);
            return foods.ToList();
        }

        public List<Food> FindFoodByChildMenu(bool cm)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Childrenmenu == cm);
            return foods.ToList();
        }

        public List<Food> FindFoodByBar(bool bar)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Bar == bar);
            return foods.ToList();
        }

        public List<Food> FindFoodByParams(string cat, bool vm, bool cm, bool bar)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Category == cat &&
                                                              needed.Vegmenu == vm &&
                                                              needed.Childrenmenu == cm &&
                                                              needed.Bar == bar);
            return foods.ToList();
        }

        public List<Food> FindFoodByParams(bool vm, bool cm, bool bar)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Vegmenu == vm &&
                                                              needed.Childrenmenu == cm &&
                                                              needed.Bar == bar);
            return foods.ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
