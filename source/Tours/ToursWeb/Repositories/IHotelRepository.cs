﻿using System.Collections.Generic;
using ToursWeb.ModelsDB;

namespace ToursWeb.Repositories
{
    public interface IHotelRepository : CrudRepository<Hotel, int>
    {
        List<Hotel> FindHotelsByCity(string city);
        List<Hotel> FindHotelsByName(string name);
        List<Hotel> FindHotelByType(string type);
        List<Hotel> FindHotelByClass(int cls);
        List<Hotel> FindHotelBySwimPool(bool sp);
    }
}
