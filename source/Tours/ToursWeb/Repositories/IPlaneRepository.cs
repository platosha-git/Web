using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;

namespace ToursWeb.Repositories
{
    public interface IPlaneRepository : CrudRepository<Planeticket, int>
    {
        List<Planeticket> FindPlanesByCityFrom(string city);
        List<Planeticket> FindPlanesByCityTo(string city);
        List<Planeticket> FindPlanesByDate(DateTime date);
        List<Planeticket> FindPlaneByLowCost(int cost);
    }
}
