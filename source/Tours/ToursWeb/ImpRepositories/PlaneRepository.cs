using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Serilog.Core;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class PlaneRepository : IPlaneRepository, IDisposable
    {
        private readonly ToursContext db;
        private readonly Logger logger;

        public PlaneRepository(ToursContext createDB, Logger logDB)
        {
            db = createDB;
            logger = logDB;
        }

        public List<Planeticket> FindAll()
        {
            List<Planeticket> planes = db.Planetickets.ToList();
            planes.RemoveAt(0);
            return planes;
        }

        public Planeticket FindByID(int id)
        {
            return db.Planetickets.Find(id);
        }

        public void Add(Planeticket obj)
        {
            try
            {
                obj.Planetid = db.Planetickets.Count() + 1;
                db.Planetickets.Add(obj);
                db.SaveChanges();
                logger.Information("+PlaneRep : Planeticket {Number} was added to Planetickets", obj.Planetid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+PlaneRep : Error trying to add planeticket to Planetickets");
            }
        }

        public void Update(Planeticket obj)
        {
            try
            {
                db.Planetickets.Update(obj);
                db.SaveChanges();
                logger.Information("+PlaneRep : Planeticket {Number} was updated at Planetickets", obj.Planetid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+PlaneRep : Error trying to update planeticket at Planetickets");
            }
}

        public void DeleteAll()
        {
            try
            {
                List<Planeticket> allPlanetickets = FindAll();
                db.Planetickets.RemoveRange(allPlanetickets);
                db.SaveChanges();
                logger.Information("+PlaneRep : All planetickets were deleted from Planetickets");
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+PlaneRep : Error trying to delete all planetickets to Planetickets");
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Planeticket planeticket = FindByID(id);
                db.Planetickets.Remove(planeticket);
                db.SaveChanges();
                logger.Information("+PlaneRep : Planeticket {Number} was deleted from Planetickets", id);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+PlaneRep : Error trying to delete planeticket {Number} from Planetickets", id);
            }
        }

        public List<Planeticket> FindPlanesByCityFrom(string city)
        {
            IQueryable<Planeticket> planeTickets = db.Planetickets.Where(needed => needed.Cityfrom.Contains(city));
            return planeTickets.ToList();
        }

        public List<Planeticket> FindPlanesByCityTo(string city)
        {
            IQueryable<Planeticket> planeTickets = db.Planetickets.Where(needed => needed.Cityto.Contains(city));
            return planeTickets.ToList();
        }

        public List<Planeticket> FindPlanesByDate(DateTime date)
        {
            DateTime dBeg = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            IQueryable<Planeticket> planeTickets = db.Planetickets.Where(needed => needed.Departuretime >= dBeg);
            return planeTickets.ToList();
        }

        public List<Planeticket> FindPlaneByLowCost(int cost)
        {
            IQueryable<Planeticket> planeTickets = db.Planetickets.Where(needed => needed.Cost <= cost && needed.Planetid > 0);
            return planeTickets.ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
