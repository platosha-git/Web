using System.Collections.Generic;
using ToursWeb.ModelsDB;

namespace ToursWeb.Repositories
{
    public interface IFoodRepository : CrudRepository<Food, int>
    {
        List<Food> FindFoodByCategory(string cat);
        List<Food> FindFoodByMenu(string menu);
        List<Food> FindFoodByBar(bool bar);
    }
}
