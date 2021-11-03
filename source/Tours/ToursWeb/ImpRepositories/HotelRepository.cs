using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Serilog.Core;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class HotelRepository : IHotelRepository, IDisposable
    {
        private readonly ToursContext db;
        private readonly Logger logger;

        public HotelRepository(ToursContext createDB, Logger logDB)
        {
            db = createDB;
            logger = logDB;
        }

        public List<Hotel> FindAll()
        {
            return db.Hotels.ToList();
        }

        public Hotel FindByID(int id)
        {
            return db.Hotels.Find(id);
        }

        public void Add(Hotel obj)
        {
            try
            {
                obj.Hotelid = db.Hotels.Count() + 1;
                db.Hotels.Add(obj);
                db.SaveChanges();
                logger.Information("+HotelRep : Hotel {Number} was added to Hotels", obj.Hotelid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+HotelRep : Error trying to add hotel to Hotels");
            }
        }

        public void Update(Hotel obj)
        {
            try
            {
                Hotel uHotel = FindByID(obj.Hotelid);
                uHotel.Name = obj.Name; uHotel.Type = obj.Type; uHotel.Class = obj.Class;
                uHotel.Swimpool = obj.Swimpool; uHotel.City = obj.City; uHotel.Cost = obj.Cost;

                db.Hotels.Update(uHotel);
                db.SaveChanges();
                logger.Information("+HotelRep : Hotel {Number} was added to Hotels", obj.Hotelid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+HotelRep : Error trying to add hotel to Hotels");
            }
        }

        public void DeleteAll()
        {
            try
            {
                List<Hotel> allHotels = FindAll();
                db.Hotels.RemoveRange(allHotels);
                db.SaveChanges();
                logger.Information("+HotelRep : All hotels were deleted from Hotels");
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+HotelRep : Error trying to delete all hotels from Hotels");
            }
}

        public void DeleteByID(int id)
        {
            try
            {
                Hotel hotel = FindByID(id);
                db.Hotels.Remove(hotel);
                db.SaveChanges();
                logger.Information("+HotelRep : Hotel {Number} was deleted from Hotels", id);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+HotelRep : Error trying to delete hotel {Number} from Hotels", id);
            }
        }
        
        public List<Hotel> FindHotelsByCity(string city)
        {
            IQueryable<Hotel> hotels = db.Hotels.Where(needed => needed.City.Equals(city));
            return hotels.ToList();
        }

        public Hotel FindHotelByName(string name)
        {
            IQueryable<Hotel> hotel = db.Hotels.Where(needed => needed.Name == name);
            return hotel.First();
        }

        public List<Hotel> FindHotelByType(string type)
        {
            IQueryable<Hotel> hotels = db.Hotels.Where(needed => needed.Type == type);
            return hotels.ToList();
        }

        public List<Hotel> FindHotelByClass(int cls)
        {
            IQueryable<Hotel> hotels = db.Hotels.Where(needed => needed.Class == cls);
            return hotels.ToList();
        }

        public List<Hotel> FindHotelBySwimPool(bool sp)
        {
            IQueryable<Hotel> hotels = db.Hotels.Where(needed => needed.Swimpool == sp);
            return hotels.ToList();
        }

        public List<Hotel> FindHotelByLowCost(int cost)
        {
            IQueryable<Hotel> hotels = db.Hotels.Where(needed => needed.Cost <= cost);
            return hotels.ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}