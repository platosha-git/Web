using Toursss.Repositories;

namespace Toursss.ComponentsBL
{
    public class GuestController : UserController
    {
        public GuestController(ITourRepository tourRep, IHotelRepository hotelRep, IFoodRepository foodRep,
                                ITransferRepository transferRep, IBusRepository busRep, IPlaneRepository planeRep, ITrainRepository trainRep,
                                IFunctionsRepository funcRep) :
            base(tourRep, hotelRep, foodRep, transferRep, busRep, planeRep, trainRep, funcRep)
        {
        }
    }
}
