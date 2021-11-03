using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Serilog.Core;

namespace ToursWeb.ImpRepositories
{
    public class FoodRepository : IFoodRepository, IDisposable
    {
        private readonly ToursContext db;
        private readonly Logger logger;

        public FoodRepository(ToursContext createDB, Logger logDB)
        {
            db = createDB;
            logger = logDB;
        }

        public List<Food> FindAll()
        {
            return db.Foods.ToList();
        }

        public Food FindByID(int id)
        {
            return db.Foods.Find(id);
        }

        public void Add(Food obj)
        {
            try 
            {
                obj.Foodid = db.Foods.Count() + 1;
                db.Foods.Add(obj);
                db.SaveChanges();
                logger.Information("+FoodRep : Food {Number} was added to Food", obj.Foodid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+FoodRep : Error trying to add food to Food");
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

                db.Foods.Update(uFood);
                db.SaveChanges();
                logger.Information("+FoodRep : Food {Number} was updated at Food", obj.Foodid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+FoodRep : Error trying to update food to Food");
            }
        }

        public void DeleteAll()
        {
            try
            {
                List<Food> allFoods = FindAll();
                db.Foods.RemoveRange(allFoods);
                db.SaveChanges();
                logger.Information("+FoodRep : All food were deleted from Food");
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+FoodRep : Error trying to delete all food from Food");
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Food food = FindByID(id);
                db.Foods.Remove(food);
                db.SaveChanges();
                logger.Information("+FoodRep : Food {Number} was deleted from Food", id);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+FoodRep : Error trying to delete food {Number} from Food", id);
            }
        }

        public List<Food> FindFoodByCategory(string cat)
        {
            IQueryable<Food> foods = db.Foods.Where(needed => needed.Category == cat);
            return foods.ToList();
        }

        public List<Food> FindFoodByVegMenu(bool vm)
        {
            IQueryable<Food> foods = db.Foods.Where(needed => needed.Vegmenu == vm);
            return foods.ToList();
        }

        public List<Food> FindFoodByChildMenu(bool cm)
        {
            IQueryable<Food> foods = db.Foods.Where(needed => needed.Childrenmenu == cm);
            return foods.ToList();
        }

        public List<Food> FindFoodByBar(bool bar)
        {
            IQueryable<Food> foods = db.Foods.Where(needed => needed.Bar == bar);
            return foods.ToList();
        }

        public List<Food> FindFoodByParams(string cat, bool vm, bool cm, bool bar)
        {
            IQueryable<Food> foods = db.Foods.Where(needed => needed.Category == cat &&
                                                              needed.Vegmenu == vm &&
                                                              needed.Childrenmenu == cm &&
                                                              needed.Bar == bar);
            return foods.ToList();
        }

        public List<Food> FindFoodByParams(bool vm, bool cm, bool bar)
        {
            IQueryable<Food> foods = db.Foods.Where(needed => needed.Vegmenu == vm &&
                                                              needed.Childrenmenu == cm &&
                                                              needed.Bar == bar);
            return foods.ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
