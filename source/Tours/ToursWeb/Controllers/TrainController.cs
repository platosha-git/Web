using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class TrainController
    {
        private readonly ITrainRepository _trainRepository;

        public TrainController(ITrainRepository trainRepository)
        {
            _trainRepository = trainRepository;
        }
        
        public List<Trainticket> GetAllTrains()
        {
            return _trainRepository.FindAll();
        }

        public Trainticket GetTrainByID(int trainID)
        {
            return _trainRepository.FindByID(trainID);
        }
        
        public void AddTrain(Trainticket ntrain)
        {
            _trainRepository.Add(ntrain);
        }

        public void UpdateTrain(Trainticket ntrain)
        {
            _trainRepository.Update(ntrain);
        }

        public void DeleteTrainByID(int trainID)
        {
            _trainRepository.DeleteByID(trainID);
        }
    }
}
