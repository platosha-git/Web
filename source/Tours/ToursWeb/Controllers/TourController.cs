using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.ModelsDB;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class TourController
    {
        private readonly ITourRepository _tourRepository;
        private readonly IHotelRepository _hotelRepository;
        
        public TourController(ITourRepository tourRepository, IHotelRepository hotelRepository)
        {
            _tourRepository = tourRepository;
            _hotelRepository = hotelRepository;
        }

        public List<Tour> GetAllTours()
        {
            return _tourRepository.FindAll();
        }
        
        public Tour GetTourByID(int tourID)
        {
            return _tourRepository.FindByID(tourID);
        }
        
        public List<Tour> GetToursByDate(DateTime beg, DateTime end)
        {
            return _tourRepository.FindTourByDate(beg, end);
        }
        
        public List<Tour> GetToursByCity(string city)
        {
            List<Hotel> hotels = _hotelRepository.FindHotelsByCity(city);
            
            List<Tour> tours = new List<Tour>();
            for (int i = 0; i < hotels.Count; i++)
            {
                Hotel curHotel = hotels[i];
                List<Tour> curTour = _tourRepository.FindToursByHotel(curHotel.Hotelid);
                tours.AddRange(curTour);
            }

            return tours;
        }

        public void AddTour(Tour ntour)
        {
            _tourRepository.Add(ntour);
        }
        
        public void UpdateTour(Tour ntour)
        {
            _tourRepository.Update(ntour);
        }
        
        public void DeleteTourByID(int tourID)
        {
            _tourRepository.DeleteByID(tourID);
        }
    }
}