using System.Collections.Generic;
using ToursWeb.Repositories;

namespace ToursWeb.ComponentsBL
{
    public class TouristController : UserController
    {
        protected IUsersRepository usersRepository;

        public TouristController(ITourRepository tourRep, IHotelRepository hotelRep, IFoodRepository foodRep,
                                ITransferRepository transferRep, IBusRepository busRep, IPlaneRepository planeRep, ITrainRepository trainRep,
                                IUsersRepository usersRep, IFunctionsRepository funcRep) :
            base(tourRep, hotelRep, foodRep, transferRep, busRep, planeRep, trainRep, funcRep)
        {
            usersRepository = usersRep;
        }

        public Tour GetTourByID(int tourID)
        {
            return tourRepository.FindByID(tourID);
        }

        public User GetAllUserInfo(int userID)
        {
            return usersRepository.FindByID(userID);
        }
    }
}
