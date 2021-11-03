using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.Repositories;

namespace ToursWeb.ComponentsBL
{
    public class UserController
    {
        protected ITourRepository tourRepository;
        protected IHotelRepository hotelRepository;
        protected IFoodRepository foodRepository;
        
        protected ITransferRepository transferRepository;
        protected IBusRepository busRepository;
        protected IPlaneRepository planeRepository;
        protected ITrainRepository trainRepository;

        protected IFunctionsRepository funcRepository;

        public UserController(ITourRepository tourRep, IHotelRepository hotelRep, IFoodRepository foodRep, 
                                ITransferRepository transferRep, IBusRepository busRep, IPlaneRepository planeRep, ITrainRepository trainRep,
                                IFunctionsRepository funcRep)
        {
            tourRepository = tourRep;
            hotelRepository = hotelRep;
            foodRepository = foodRep;

            transferRepository = transferRep;
            busRepository = busRep;
            planeRepository = planeRep;
            trainRepository = trainRep;

            funcRepository = funcRep;
        }

        /*--------------------------------------------------------------
         *                          Tours
         * -----------------------------------------------------------*/
        public List<Tour> GetAllTours()
        {
            return tourRepository.FindAll();
        }

        public FullUserTour GetFullTour(int TID)
        {
            return funcRepository.GetFullTour(TID);
        }

        public List<Tour> GetToursByDate(DateTime beg, DateTime end)
        {
            return tourRepository.FindTourByDate(beg, end);
        }

        public List<Tour> GetToursByCity(string city)
        {
            List<Hotel> hotels = hotelRepository.FindHotelsByCity(city);
            List<Tour> tours = new List<Tour>();
            for (int i = 0; i < hotels.Count; i++)
            {
                Hotel curHotel = hotels[i];
                List<Tour> curTour = tourRepository.FindToursByHotel(curHotel.Hotelid);
                tours.AddRange(curTour);
            }

            return tours;
        }


        /*--------------------------------------------------------------
         *                          Hotels
         * -----------------------------------------------------------*/
        public List<Hotel> GetAllHotels()
        {
            return hotelRepository.FindAll();
        }

        public Hotel GetHotelByID(int hotelID)
        {
            return hotelRepository.FindByID(hotelID);
        }

        public List<Hotel> GetHotelsByCity(string city)
        {
            return hotelRepository.FindHotelsByCity(city);
        }

        public Hotel GetHotelByName(string name)
        {
            return hotelRepository.FindHotelByName(name);
        }

        public List<Hotel> GetHotelsByType(string type)
        {
            return hotelRepository.FindHotelByType(type);
        }

        public List<Hotel> GetHotelsByClass(int cls)
        {
            return hotelRepository.FindHotelByClass(cls);
        }

        public List<Hotel> GetHotelsBySwimPool(bool sp)
        {
            return hotelRepository.FindHotelBySwimPool(sp);
        }

        /*--------------------------------------------------------------
         *                          Food
         * -----------------------------------------------------------*/
        public List<Food> GetAllFood()
        {
            return foodRepository.FindAll();
        }

        public Food GetFoodByID(int foodID)
        {
            return foodRepository.FindByID(foodID);
        }

        public List<Food> GetFoodByCategory(string cat)
        {
            return foodRepository.FindFoodByCategory(cat);
        }

        public List<Food> GetFoodByVegMenu(bool vm)
        {
            return foodRepository.FindFoodByVegMenu(vm);
        }

        public List<Food> GetFoodByChildMenu(bool cm)
        {
            return foodRepository.FindFoodByChildMenu(cm);
        }

        public List<Food> GetFoodByBar(bool bar)
        {
            return foodRepository.FindFoodByBar(bar);
        }



        public Transfer GetTransferByID(int transfID)
        {
            return transferRepository.FindByID(transfID);
        }

        /*--------------------------------------------------------------
         *                          BusTicket
         * -----------------------------------------------------------*/
        public List<Busticket> GetAllBuses()
        {
            return busRepository.FindAll();
        }

        public Busticket GetBusByID(int busID)
        {
            return busRepository.FindByID(busID);
        }

        public List<Busticket> GetBusesByCityFrom(string city)
        {
            return busRepository.FindBusesByCityFrom(city);
        }

        public List<Busticket> GetBusesByCityTo(string city)
        {
            return busRepository.FindBusesByCityTo(city);
        }

        public List<Busticket> GetBusesByDate(DateTime date)
        {
            return busRepository.FindBusesByDate(date);
        }

        /*--------------------------------------------------------------
         *                          PlaneTicket
         * -----------------------------------------------------------*/
        public List<Planeticket> GetAllPlanes()
        {
            return planeRepository.FindAll();
        }

        public Planeticket GetPlaneByID(int planeID)
        {
            return planeRepository.FindByID(planeID);
        }

        public List<Planeticket> GetPlanesByCityFrom(string city)
        {
            return planeRepository.FindPlanesByCityFrom(city);
        }

        public List<Planeticket> GetPlanesByCityTo(string city)
        {
            return planeRepository.FindPlanesByCityTo(city);
        }

        public List<Planeticket> GetPlanesByDate(DateTime date)
        {
            return planeRepository.FindPlanesByDate(date);
        }

        /*--------------------------------------------------------------
         *                          TrainTicket
         * -----------------------------------------------------------*/
        public List<Trainticket> GetAllTrains()
        {
            return trainRepository.FindAll();
        }

        public Trainticket GetTrainByID(int trainID)
        {
            return trainRepository.FindByID(trainID);
        }

        public List<Trainticket> GetTrainsByCityFrom(string city)
        {
            return trainRepository.FindTrainsByCityFrom(city);
        }

        public List<Trainticket> GetTrainsByCityTo(string city)
        {
            return trainRepository.FindTrainsByCityTo(city);
        }

        public List<Trainticket> GetTrainsByDate(DateTime date)
        {
            return trainRepository.FindTrainsByDate(date);
        }
    }
}
