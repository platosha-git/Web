using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class TourRepository : ITourRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<TourRepository> _logger;

        public TourRepository(ToursContext createDB, ILogger<TourRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        public List<Tour> FindAll()
        {
            return _db.Tours.ToList();
        }

        public Tour FindByID(int id)
        {
            return _db.Tours.Find(id);
        }

        public ExitCode Add(Tour obj)
        {
            try
            {
                obj.Tourid = _db.Tours.Count() + 1;
                _db.Tours.Add(obj);
                _db.SaveChanges();
                _logger.LogInformation("+TourRep : Tour {Number} was added to Tours", obj.Tourid);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TourRep : Error trying to add tour to Tours");
                return ExitCode.Error;
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

                _db.Tours.Update(uTour);
                _db.SaveChanges();
                _logger.LogInformation("+TourRep : Tours {Number} was updated at Tours", obj.Tourid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TourRep : Error trying to update tour at Tours");
            }
        }

        public bool ChangeCost(int id, int diff)
        {
            try
            {
                Tour uTour = FindByID(id);
                uTour.Cost += diff;
                
                _db.Tours.Update(uTour);
                _db.SaveChanges();
                _logger.LogInformation("+TourRep : Cost tours {Number} was updated at Tours", id);

                return true;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TourRep : Error trying to update tour cost at Tours");
                return false;
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Tour tour = FindByID(id);
                _db.Tours.Remove(tour);
                _db.SaveChanges();
                _logger.LogInformation("+TourRep : Tours {Number} was deleted from Tours", tour.Tourid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TourRep : Error trying to delete tour from Tours");
            }
        }

        public List<Tour> FindToursByDate(DateTime b, DateTime e)
        {
            IQueryable<Tour> tours = _db.Tours.Where(needed => needed.Datebegin >= b && needed.Dateend <= e);
            return tours.ToList();
        }

        public List<Tour> FindToursByHotel(int hotelID)
        {
            IQueryable<Tour> tours = _db.Tours.Where(needed => needed.Hotel == hotelID);
            return tours.ToList();
        }
        
        public List<Tour> FindToursByFood(int foodID)
        {
            IQueryable<Tour> tours = _db.Tours.Where(needed => needed.Food == foodID);
            return tours.ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
