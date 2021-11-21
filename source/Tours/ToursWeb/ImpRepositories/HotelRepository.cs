using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class HotelRepository : IHotelRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<HotelRepository> _logger;

        public HotelRepository(ToursContext createDB, ILogger<HotelRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        public List<Hotel> FindAll()
        {
            return _db.Hotels.ToList();
        }

        public Hotel FindByID(int id)
        {
            return _db.Hotels.Find(id);
        }

        public ExitCode Add(Hotel obj)
        {
            try
            {
                obj.Hotelid = _db.Hotels.Count() + 1;
                _db.Hotels.Add(obj);
                _db.SaveChanges();
                _logger.LogInformation("+HotelRep : Hotel {Number} was added to Hotels", obj.Hotelid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+HotelRep : Constraint violation when trying to add hotel to Hotel");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+HotelRep : Error trying to add hotel to Hotels");
                return ExitCode.Error;
            }
        }

        public ExitCode Update(Hotel obj)
        {
            try
            {
                Hotel uHotel = FindByID(obj.Hotelid);
                uHotel.Name = obj.Name; uHotel.Type = obj.Type; uHotel.Class = obj.Class;
                uHotel.Swimpool = obj.Swimpool; uHotel.City = obj.City; uHotel.Cost = obj.Cost;

                _db.Hotels.Update(uHotel);
                _db.SaveChanges();
                _logger.LogInformation("+HotelRep : Hotel {Number} was updated in Hotels", obj.Hotelid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+HotelRep : Constraint violation when trying to update hotel in Hotel");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+HotelRep : Error trying to update hotel in Hotels");
                return ExitCode.Error;
            }
        }

        public ExitCode DeleteByID(int id)
        {
            try
            {
                Hotel hotel = FindByID(id);
                _db.Hotels.Remove(hotel);
                _db.SaveChanges();
                _logger.LogInformation("+HotelRep : Hotel {Number} was deleted from Hotels", id);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+HotelRep : Error trying to delete hotel {Number} from Hotels", id);
                return ExitCode.Error;
            }
        }
        
        public List<Hotel> FindHotelsByCity(string city)
        {
            IQueryable<Hotel> hotels = _db.Hotels.Where(needed => needed.City.Equals(city));
            return hotels.ToList();
        }

        public List<Hotel> FindHotelsByName(string name)
        {
            IQueryable<Hotel> hotel = _db.Hotels.Where(needed => needed.Name == name);
            return hotel.ToList();
        }

        public List<Hotel> FindHotelByType(string type)
        {
            IQueryable<Hotel> hotels = _db.Hotels.Where(needed => needed.Type == type);
            return hotels.ToList();
        }

        public List<Hotel> FindHotelByClass(int cls)
        {
            IQueryable<Hotel> hotels = _db.Hotels.Where(needed => needed.Class == cls);
            return hotels.ToList();
        }

        public List<Hotel> FindHotelBySwimPool(bool sp)
        {
            IQueryable<Hotel> hotels = _db.Hotels.Where(needed => needed.Swimpool == sp);
            return hotels.ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}