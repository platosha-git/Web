using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Serilog.Core;

namespace ToursWeb.ImpRepositories
{
    public class BusRepository : IBusRepository, IDisposable
    {
        private readonly ToursContext db;
        private readonly Logger logger;

        public BusRepository(ToursContext createDB, Logger logDB)
        {
            db = createDB;
            logger = logDB;
        }

        public List<Busticket> FindAll()
        {
            List<Busticket> buses = db.Bustickets.ToList();
            buses.RemoveAt(0);
            return buses;
        }

        public Busticket FindByID(int id)
        {
            return db.Bustickets.Find(id);
        }

        public void Add(Busticket obj)
        {
            try
            {
                obj.Bustid = db.Bustickets.Count();
                db.Bustickets.Add(obj);
                db.SaveChanges();
                logger.Information("+BusRep : Busticket {Number} was added to Bustickets", obj.Bustid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+BusRep : Error trying to add busticket to Bustickets");
            }
        }

        public void Update(Busticket obj)
        {
            try
            {
                db.Bustickets.Update(obj);
                logger.Information("+BusRep : Busticket {Number} was updated at Bustickets", obj.Bustid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+BusRep : Error trying to update busticket at Bustickets");
            }
        }

        public void DeleteAll()
        {
            try
            {
                List<Busticket> allBustickets = FindAll();
                db.Bustickets.RemoveRange(allBustickets);
                db.SaveChanges();
                logger.Information("+BusRep : All bustickets were deleted from Bustickets");
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+BusRep : Error trying to delete all bustickets from Bustickets");
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Busticket busticket = FindByID(id);
                db.Bustickets.Remove(busticket);
                db.SaveChanges();
                logger.Information("+BusRep : Busticket {Number} was deleted from Bustickets", id);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+BusRep : Error trying to delete busticket {Number} from Bustickets", id);
            }
        }

        public List<Busticket> FindBusesByCityFrom(string city)
        {
            IQueryable<Busticket> busTickets = db.Bustickets.Where(needed => needed.Cityfrom.Contains(city));
            return busTickets.ToList();
        }

        public List<Busticket> FindBusesByCityTo(string city)
        {
            IQueryable<Busticket> busTickets = db.Bustickets.Where(needed => needed.Cityto.Contains(city));
            return busTickets.ToList();
        }

        public List<Busticket> FindBusesByDate(DateTime date)
        {
            DateTime dBeg = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            IQueryable<Busticket> busTickets = db.Bustickets.Where(needed => needed.Departuretime >= dBeg);
            return busTickets.ToList();
        }

        public List<Busticket> FindBusByLowCost(int cost)
        {
            IQueryable<Busticket> busTickets = db.Bustickets.Where(needed => needed.Cost <= cost && needed.Bustid > 0);
            return busTickets.ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
        