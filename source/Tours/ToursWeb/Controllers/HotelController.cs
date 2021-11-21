using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.ModelsDB;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class HotelController
    {
        private readonly IHotelRepository _hotelRepository;
        
        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public List<Hotel> GetAllHotels()
        {
            return _hotelRepository.FindAll();
        }

        public Hotel GetHotelByID(int hotelID)
        {
            return _hotelRepository.FindByID(hotelID);
        }

        public List<Hotel> GetHotelsByCity(string city)
        {
            return _hotelRepository.FindHotelsByCity(city);
        }

        public List<Hotel> GetHotelsByType(string type)
        {
            return _hotelRepository.FindHotelByType(type);
        }

        public List<Hotel> GetHotelsByClass(int cls)
        {
            return _hotelRepository.FindHotelByClass(cls);
        }

        public List<Hotel> GetHotelsBySwimPool(bool sp)
        {
            return _hotelRepository.FindHotelBySwimPool(sp);
        }
        
        public ExitCode AddHotel(Hotel nhotel)
        {
            return _hotelRepository.Add(nhotel);
        }
        
        public ExitCode UpdateHotel(Hotel nhotel)
        {
            return _hotelRepository.Update(nhotel);
        }
        
        public ExitCode DeleteHotelByID(int hotelID)
        {
            return _hotelRepository.DeleteByID(hotelID);
        }
    }
}