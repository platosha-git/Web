using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class TrainRepository : ITrainRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<TrainRepository> _logger;

        public TrainRepository(ToursContext createDB, ILogger<TrainRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        public List<Trainticket> FindAll()
        {
            List<Trainticket> trains = _db.Traintickets.ToList();
            trains.RemoveAt(0);
            return trains;
        }

        public Trainticket FindByID(int id)
        {
            return _db.Traintickets.Find(id);
        }

        public void Add(Trainticket obj)
        {
            try
            {
                obj.Traintid = _db.Traintickets.Count() + 1;
                _db.Traintickets.Add(obj);
                _db.SaveChanges();
                _logger.LogInformation("+TrainRep : Trainticket {Number} was added to Traintickets", obj.Traintid);
            }
            catch (Exception err)
            {
                _logger.LogError(err.Message, "+TrainRep : Error trying to add trainticket to Traintickets");
            }
        }

        public void Update(Trainticket obj)
        {
            try
            {
                _db.Traintickets.Update(obj);
                _db.SaveChanges();
                _logger.LogInformation("+TrainRep : Trainticket {Number} was updated at Traintickets", obj.Traintid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TrainRep : Error trying to update trainticket at Traintickets");
            }
        }

        public void DeleteAll()
        {
            try
            {
                List<Trainticket> allTrainTicket = FindAll();
                _db.Traintickets.RemoveRange(allTrainTicket);
                _db.SaveChanges();
                _logger.LogInformation("+TrainRep : All traintickets were deleted from Traintickets");
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TrainRep : Error trying to delete all traintickets from Traintickets");
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Trainticket trainTicket = FindByID(id);
                _db.Traintickets.Remove(trainTicket);
                _db.SaveChanges();
                _logger.LogInformation("+TrainRep : Trainticket {Number} was deleted from Traintickets", id);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TrainRep : Error trying to delete trainticket {Number} from Traintickets", id);
            }
        }

        public List<Trainticket> FindTrainsByCityFrom(string city)
        {
            IQueryable<Trainticket> trainTickets = _db.Traintickets.Where(needed => needed.Cityfrom.Contains(city));
            return trainTickets.ToList();
        }

        public List<Trainticket> FindTrainsByCityTo(string city)
        {
            IQueryable<Trainticket> trainTickets = _db.Traintickets.Where(needed => needed.Cityto.Contains(city));
            return trainTickets.ToList();
        }

        public List<Trainticket> FindTrainsByDate(DateTime date)
        {
            DateTime dBeg = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            IQueryable<Trainticket> trainTickets = _db.Traintickets.Where(needed => needed.Departuretime >= dBeg);
            return trainTickets.ToList();
        }

        public List<Trainticket> FindTrainByLowCost(int cost)
        {
            IQueryable<Trainticket> trainTickets = _db.Traintickets.Where(needed => needed.Cost <= cost && needed.Traintid > 0);
            return trainTickets.ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
