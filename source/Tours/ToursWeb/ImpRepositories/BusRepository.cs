using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class BusRepository : IBusRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<BusRepository> _logger;

        public BusRepository(ToursContext createDB, ILogger<BusRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        public List<Busticket> FindAll()
        {
            List<Busticket> buses = _db.Bustickets.ToList();
            buses.RemoveAt(0);
            return buses;
        }

        public Busticket FindByID(int id)
        {
            return _db.Bustickets.Find(id);
        }

        public void Add(Busticket obj)
        {
            try
            {
                obj.Bustid = _db.Bustickets.Count();
                _db.Bustickets.Add(obj);
                _db.SaveChanges();
                _logger.LogInformation("+BusRep : Busticket {Number} was added to Bustickets", obj.Bustid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+BusRep : Error trying to add busticket to Bustickets");
            }
        }

        public void Update(Busticket obj)
        {
            try
            {
                _db.Bustickets.Update(obj);
                _logger.LogInformation("+BusRep : Busticket {Number} was updated at Bustickets", obj.Bustid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+BusRep : Error trying to update busticket at Bustickets");
            }
        }

        public void DeleteAll()
        {
            try
            {
                List<Busticket> allBustickets = FindAll();
                _db.Bustickets.RemoveRange(allBustickets);
                _db.SaveChanges();
                _logger.LogInformation("+BusRep : All bustickets were deleted from Bustickets");
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+BusRep : Error trying to delete all bustickets from Bustickets");
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Busticket busticket = FindByID(id);
                _db.Bustickets.Remove(busticket);
                _db.SaveChanges();
                _logger.LogInformation("+BusRep : Busticket {Number} was deleted from Bustickets", id);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+BusRep : Error trying to delete busticket {Number} from Bustickets", id);
            }
        }

        public List<Busticket> FindBusesByCityFrom(string city)
        {
            IQueryable<Busticket> busTickets = _db.Bustickets.Where(needed => needed.Cityfrom.Contains(city));
            return busTickets.ToList();
        }

        public List<Busticket> FindBusesByCityTo(string city)
        {
            IQueryable<Busticket> busTickets = _db.Bustickets.Where(needed => needed.Cityto.Contains(city));
            return busTickets.ToList();
        }

        public List<Busticket> FindBusesByDate(DateTime date)
        {
            DateTime dBeg = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            IQueryable<Busticket> busTickets = _db.Bustickets.Where(needed => needed.Departuretime >= dBeg);
            return busTickets.ToList();
        }

        public List<Busticket> FindBusByLowCost(int cost)
        {
            IQueryable<Busticket> busTickets = _db.Bustickets.Where(needed => needed.Cost <= cost && needed.Bustid > 0);
            return busTickets.ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
        