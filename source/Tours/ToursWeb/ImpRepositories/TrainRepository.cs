using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Serilog.Core;

namespace ToursWeb.ImpRepositories
{
    public class TrainRepository : ITrainRepository, IDisposable
    {
        private readonly ToursContext db;
        private readonly Logger logger;

        public TrainRepository(ToursContext createDB, Logger logDB)
        {
            db = createDB;
            logger = logDB;
        }

        public List<Trainticket> FindAll()
        {
            List<Trainticket> trains = db.Traintickets.ToList();
            trains.RemoveAt(0);
            return trains;
        }

        public Trainticket FindByID(int id)
        {
            return db.Traintickets.Find(id);
        }

        public void Add(Trainticket obj)
        {
            try
            {
                obj.Traintid = db.Traintickets.Count() + 1;
                db.Traintickets.Add(obj);
                db.SaveChanges();
                logger.Information("+TrainRep : Trainticket {Number} was added to Traintickets", obj.Traintid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TrainRep : Error trying to add trainticket to Traintickets");
            }
        }

        public void Update(Trainticket obj)
        {
            try
            {
                db.Traintickets.Update(obj);
                db.SaveChanges();
                logger.Information("+TrainRep : Trainticket {Number} was updated at Traintickets", obj.Traintid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TrainRep : Error trying to update trainticket at Traintickets");
            }
        }

        public void DeleteAll()
        {
            try
            {
                List<Trainticket> allTrainTicket = FindAll();
                db.Traintickets.RemoveRange(allTrainTicket);
                db.SaveChanges();
                logger.Information("+TrainRep : All traintickets were deleted from Traintickets");
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TrainRep : Error trying to delete all traintickets from Traintickets");
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Trainticket trainTicket = FindByID(id);
                db.Traintickets.Remove(trainTicket);
                db.SaveChanges();
                logger.Information("+TrainRep : Trainticket {Number} was deleted from Traintickets", id);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TrainRep : Error trying to delete trainticket {Number} from Traintickets", id);
            }
        }

        public List<Trainticket> FindTrainsByCityFrom(string city)
        {
            IQueryable<Trainticket> trainTickets = db.Traintickets.Where(needed => needed.Cityfrom.Contains(city));
            return trainTickets.ToList();
        }

        public List<Trainticket> FindTrainsByCityTo(string city)
        {
            IQueryable<Trainticket> trainTickets = db.Traintickets.Where(needed => needed.Cityto.Contains(city));
            return trainTickets.ToList();
        }

        public List<Trainticket> FindTrainsByDate(DateTime date)
        {
            DateTime dBeg = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            IQueryable<Trainticket> trainTickets = db.Traintickets.Where(needed => needed.Departuretime >= dBeg);
            return trainTickets.ToList();
        }

        public List<Trainticket> FindTrainByLowCost(int cost)
        {
            IQueryable<Trainticket> trainTickets = db.Traintickets.Where(needed => needed.Cost <= cost && needed.Traintid > 0);
            return trainTickets.ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
