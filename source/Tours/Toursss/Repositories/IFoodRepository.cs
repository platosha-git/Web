using System.Collections.Generic;

namespace Toursss.Repositories
{
    public interface IFoodRepository : CrudRepository<Food, int>
    {
        List<Food> FindFoodByCategory(string cat);
        List<Food> FindFoodByVegMenu(bool vm);
        List<Food> FindFoodByChildMenu(bool cm);
        List<Food> FindFoodByBar(bool bar);
        List<Food> FindFoodByParams(string cat, bool vm, bool cm, bool bar);
        List<Food> FindFoodByParams(bool vm, bool cm, bool bar);
    }
}
