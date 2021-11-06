using System.Collections.Generic;
using ToursWeb.Repositories;

namespace ToursWeb.ComponentsBL
{
    public class ManagerController : UserController
    {
        protected IUsersRepository usersRepository;
        
        public ManagerController(ITourRepository tourRep, IHotelRepository hotelRep, IFoodRepository foodRep,
                                ITransferRepository transferRep, IBusRepository busRep, IPlaneRepository planeRep, ITrainRepository trainRep,
                                IUsersRepository usersRep, IFunctionsRepository funcRep) :
            base(tourRep, hotelRep, foodRep, transferRep, busRep, planeRep, trainRep, funcRep)
        {
            usersRepository = usersRep;
        }

        public List<User> GetAllUsers()
        {
            return usersRepository.FindAll();
        }
        public Tour GetTourByID(int tourID)
        {
            return tourRepository.FindByID(tourID);
        }

        /*--------------------------------------------------------------
         *                          Add
         * -----------------------------------------------------------*/
        public void AddTour(Tour ntour)
        {
            tourRepository.Add(ntour);
        }

        public void AddHotel(Hotel nhotel)
        {
            hotelRepository.Add(nhotel);
        }

        public void AddFood(Food nfood)
        {
            foodRepository.Add(nfood);
        }

       
        /*--------------------------------------------------------------
         *                          Update
         * -----------------------------------------------------------*/
        public void UpdateTour(Tour ntour)
        {
            tourRepository.Update(ntour);
        }

        public void UpdateHotel(Hotel nhotel)
        {
            hotelRepository.Update(nhotel);
        }

        public void UpdateFood(Food nfood)
        {
            foodRepository.Update(nfood);
        }


        /*--------------------------------------------------------------
         *                          Delete
         * -----------------------------------------------------------*/
        public void DeleteTourByID(int tourID)
        {
            tourRepository.DeleteByID(tourID);
        }

        public void DeleteHotelByID(int hotelID)
        {
            hotelRepository.DeleteByID(hotelID);
        }

        public void DeleteHotelByName(string name)
        {
            Hotel hotel = hotelRepository.FindHotelByName(name);
            hotelRepository.DeleteByID(hotel.Hotelid);
        }

        public void DeleteFoodByID(int foodID)
        {
            foodRepository.DeleteByID(foodID);
        }
    }
}
