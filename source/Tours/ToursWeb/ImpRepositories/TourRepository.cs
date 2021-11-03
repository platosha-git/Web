using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Serilog.Core;
using Microsoft.EntityFrameworkCore;

namespace ToursWeb.ImpRepositories
{
    public class TourRepository : ITourRepository, IDisposable
    {
        private readonly ToursContext db;
        private Logger logger;

        public TourRepository(ToursContext createDB, Logger logDB)
        {
            db = createDB;
            logger = logDB;
        }

        public List<Tour> FindAll()
        {
            return db.Tours.ToList();
        }

        public Tour FindByID(int id)
        {
            return db.Tours.Find(id);
        }

        public void Add(Tour obj)
        {
            try
            {
                obj.Tourid = db.Tours.Count() + 1;
                db.Tours.Add(obj);
                db.SaveChanges();
                logger.Information("+TourRep : Tourss {Number} was added to Tours", obj.Tourid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TourRep : Error trying to add tour to Tours");
            }
        }

        public void Update(Tour obj)
        {
            try
            {
                Tour uTour = FindByID(obj.Tourid);
                uTour.Food = obj.Food; uTour.Hotel = obj.Hotel; uTour.Transfer = obj.Transfer;
                uTour.Cost = obj.Cost;
                uTour.Datebegin = obj.Datebegin;
                uTour.Dateend = obj.Dateend;

                db.Tours.Update(uTour);
                db.SaveChanges();
                logger.Information("+TourRep : Tourss {Number} was updated at Tours", obj.Tourid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TourRep : Error trying to update tour at Tours");
            }
        }

        public void DeleteAll()
        {
            try
            {
                List<Tour> allTours = FindAll();
                db.Tours.RemoveRange(allTours);
                db.SaveChanges();
                logger.Information("+TourRep : All tours were deleted from Tours");
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TourRep : Error trying to delete all tours from Tours");
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Tour tour = FindByID(id);
                db.Tours.Remove(tour);
                db.SaveChanges();
                logger.Information("+TourRep : Tourss {Number} was deleted from Tours", tour.Tourid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TourRep : Error trying to delete tour from Tours");
            }
        }

        public List<Tour> FindTourByDate(DateTime b, DateTime e)
        {
            IQueryable<Tour> tours = db.Tours.Where(needed => needed.Datebegin >= b && needed.Dateend <= e);
            return tours.ToList();
        }

        public List<Tour> FindToursByHotel(int hotelID)
        {
            IQueryable<Tour> tours = db.Tours.Where(needed => needed.Hotel == hotelID);
            return tours.ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
