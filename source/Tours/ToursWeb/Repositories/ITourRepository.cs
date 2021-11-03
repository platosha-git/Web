using System;
using System.Collections.Generic;

namespace ToursWeb.Repositories
{
    public interface ITourRepository : CrudRepository<Tour, int>
    {
        List<Tour> FindTourByDate(DateTime b, DateTime e);
        List<Tour> FindToursByHotel(int hotelID);
    }
}
