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
        private readonly IFoodRepository _foodRepository;
        private readonly ITransferRepository _transferRepository;

        public TourController(ITourRepository tourRepository, IHotelRepository hotelRepository,
            IFoodRepository foodRepository, ITransferRepository transferRepository)
        {
            _tourRepository = tourRepository;
            _hotelRepository = hotelRepository;
            _foodRepository = foodRepository;
            _transferRepository = transferRepository;
        }

        public List<UserTour> ToUserTour(List<Tour> tours)
        {
            List<UserTour> userTours = new List<UserTour>();
            foreach (var tour in tours)
            {
                Hotel hotel = _hotelRepository.FindByID(tour.Hotel); 
                Food food = _foodRepository.FindByID(tour.Food);
                Transfer transfer = _transferRepository.FindByID(tour.Transfer);
                
                UserTour userTourDTO = new UserTour(tour, hotel, food, transfer);
                userTours.Add(userTourDTO);
            }
            return userTours;
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
            return _tourRepository.FindToursByDate(beg, end);
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
        
        public List<Tour> GetToursByCityName(string city, string name)
        {
            List<Hotel> hotelsCity = _hotelRepository.FindHotelsByCity(city);
            List<Hotel> hotelsName = _hotelRepository.FindHotelsByName(name);
            List<Hotel> hotels = hotelsCity.Intersect(hotelsName).ToList();

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