using System.Collections.Generic;
using ToursWeb.Repositories;
using ToursWeb.ModelsDB;

namespace ToursWeb.ComponentsBL
{
    public class TransferManagerController : UserController
    {
        public TransferManagerController(ITourRepository tourRep, IHotelRepository hotelRep, IFoodRepository foodRep,
                                        ITransferRepository transferRep, IBusRepository busRep, IPlaneRepository planeRep, ITrainRepository trainRep,
                                        IFunctionsRepository funcRep) :
            base(tourRep, hotelRep, foodRep, transferRep, busRep, planeRep, trainRep, funcRep)
        {
        }

        public List<Transfer> GetAllTransfer()
        {
            return transferRepository.FindAll();
        }


        /*--------------------------------------------------------------
         *                          Add
         * -----------------------------------------------------------*/
        public void AddTransfer(Transfer ntran)
        {
            transferRepository.Add(ntran);
        }

        public void AddBus(Busticket nbus)
        {
            busRepository.Add(nbus);
        }

        public void AddPlane(Planeticket nplane)
        {
            planeRepository.Add(nplane);
        }

        public void AddTrain(Trainticket ntrain)
        {
            trainRepository.Add(ntrain);
        }


        /*--------------------------------------------------------------
         *                          Update
         * -----------------------------------------------------------*/
        public void UpdateTransfer(Transfer ntran)
        {
            transferRepository.Update(ntran);
        }

        public void UpdateBus(Busticket nbus)
        {
            busRepository.Update(nbus);
        }

        public void UpdatePlane(Planeticket nplane)
        {
            planeRepository.Update(nplane);
        }

        public void UpdateTrain(Trainticket ntrain)
        {
            trainRepository.Update(ntrain);
        }

        /*--------------------------------------------------------------
         *                          Delete
         * -----------------------------------------------------------*/
        public void DeleteTransferByID(int tranID)
        {
            transferRepository.DeleteByID(tranID);
        }

        public void DeleteBusByID(int busID)
        {
            busRepository.DeleteByID(busID);
        }

        public void DeletePlaneByID(int planeID)
        {
            planeRepository.DeleteByID(planeID);
        }

        public void DeleteTrainByID(int trainID)
        {
            trainRepository.DeleteByID(trainID);
        }
    }
}
