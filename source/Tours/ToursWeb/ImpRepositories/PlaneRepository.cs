using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class PlaneRepository : IPlaneRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<PlaneRepository> _logger;

        public PlaneRepository(ToursContext createDB, ILogger<PlaneRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        public List<Planeticket> FindAll()
        {
            List<Planeticket> planes = _db.Planetickets.ToList();
            planes.RemoveAt(0);
            return planes;
        }

        public Planeticket FindByID(int id)
        {
            return _db.Planetickets.Find(id);
        }

        public void Add(Planeticket obj)
        {
            try
            {
                obj.Planetid = _db.Planetickets.Count() + 1;
                _db.Planetickets.Add(obj);
                _db.SaveChanges();
                _logger.LogInformation("+PlaneRep : Planeticket {Number} was added to Planetickets", obj.Planetid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+PlaneRep : Error trying to add planeticket to Planetickets");
            }
        }

        public void Update(Planeticket obj)
        {
            try
            {
                _db.Planetickets.Update(obj);
                _db.SaveChanges();
                _logger.LogInformation("+PlaneRep : Planeticket {Number} was updated at Planetickets", obj.Planetid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+PlaneRep : Error trying to update planeticket at Planetickets");
            }
}

        public void DeleteAll()
        {
            try
            {
                List<Planeticket> allPlanetickets = FindAll();
                _db.Planetickets.RemoveRange(allPlanetickets);
                _db.SaveChanges();
                _logger.LogInformation("+PlaneRep : All planetickets were deleted from Planetickets");
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+PlaneRep : Error trying to delete all planetickets to Planetickets");
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Planeticket planeticket = FindByID(id);
                _db.Planetickets.Remove(planeticket);
                _db.SaveChanges();
                _logger.LogInformation("+PlaneRep : Planeticket {Number} was deleted from Planetickets", id);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+PlaneRep : Error trying to delete planeticket {Number} from Planetickets", id);
            }
        }

        public List<Planeticket> FindPlanesByCityFrom(string city)
        {
            IQueryable<Planeticket> planeTickets = _db.Planetickets.Where(needed => needed.Cityfrom.Contains(city));
            return planeTickets.ToList();
        }

        public List<Planeticket> FindPlanesByCityTo(string city)
        {
            IQueryable<Planeticket> planeTickets = _db.Planetickets.Where(needed => needed.Cityto.Contains(city));
            return planeTickets.ToList();
        }

        public List<Planeticket> FindPlanesByDate(DateTime date)
        {
            DateTime dBeg = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            IQueryable<Planeticket> planeTickets = _db.Planetickets.Where(needed => needed.Departuretime >= dBeg);
            return planeTickets.ToList();
        }

        public List<Planeticket> FindPlaneByLowCost(int cost)
        {
            IQueryable<Planeticket> planeTickets = _db.Planetickets.Where(needed => needed.Cost <= cost && needed.Planetid > 0);
            return planeTickets.ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
