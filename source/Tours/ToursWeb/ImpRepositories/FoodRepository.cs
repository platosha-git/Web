using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using Npgsql;
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

        public ExitCode Add(Food obj)
        {
            try
            {
                obj.Foodid = _db.Foods.Count() + 1;
                _db.Foods.Add(obj);
                _db.SaveChanges();
                _logger.LogInformation("+FoodRep : Food {Number} was added to Food", obj.Foodid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+FoodRep : Constraint violation when trying to add food to Food");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+FoodRep : Error trying to add food to Food");
                return ExitCode.Error;
            }
        }

        public ExitCode Update(Food obj)
        {
            try
            {
                Food uFood = FindByID(obj.Foodid);
                uFood.Category = obj.Category; 
                uFood.Menu = obj.Menu; 
                uFood.Bar = obj.Bar; 
                uFood.Cost = obj.Cost;

                _db.Foods.Update(uFood);
                _db.SaveChanges();
                _logger.LogInformation("+FoodRep : Food {Number} was updated at Food", obj.Foodid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+FoodRep : Constraint violation when trying to update food to Food");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+FoodRep : Error trying to update food to Food");
                return ExitCode.Error;
            }
        }

        public ExitCode DeleteByID(int id)
        {
            try
            {
                Food food = FindByID(id);
                _db.Foods.Remove(food);
                _db.SaveChanges();
                _logger.LogInformation("+FoodRep : Food {Number} was deleted from Food", id);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+FoodRep : Error trying to delete food {Number} from Food", id);
                return ExitCode.Error;
            }
        }

        public List<Food> FindFoodByCategory(string cat)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Category.Equals(cat));
            return foods.ToList();
        }

        public List<Food> FindFoodByMenu(string menu)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Menu.Equals(menu));
            return foods.ToList();
        }

        public List<Food> FindFoodByBar(bool bar)
        {
            IQueryable<Food> foods = _db.Foods.Where(needed => needed.Bar == bar);
            return foods.ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
