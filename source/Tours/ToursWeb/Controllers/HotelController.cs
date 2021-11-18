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
        
        public void AddHotel(Hotel nhotel)
        {
            _hotelRepository.Add(nhotel);
        }
        
        public void UpdateHotel(Hotel nhotel)
        {
            _hotelRepository.Update(nhotel);
        }
        
        public void DeleteHotelByID(int hotelID)
        {
            _hotelRepository.DeleteByID(hotelID);
        }
    }
}