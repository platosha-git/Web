using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;

namespace ToursWeb.Repositories
{
    public interface ITourRepository : CrudRepository<Tour, int>
    {
        bool ChangeCost(int id, int diff);
        List<Tour> FindTourByDate(DateTime b, DateTime e);
        List<Tour> FindToursByHotel(int hotelID);
    }
}
