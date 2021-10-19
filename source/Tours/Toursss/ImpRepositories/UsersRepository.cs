using System;
using System.Collections.Generic;
using System.Linq;
using Toursss.Repositories;
using Serilog.Core;

namespace Toursss.ImpRepositories
{
    public class UsersRepository : IUsersRepository, IDisposable
    {
        private readonly ToursContext db;
        private readonly Logger logger;

        public UsersRepository(ToursContext createDB, Logger logDB)
        {
            db = createDB;
            logger = logDB;
        }

        public List<User> FindAll()
        {
            return db.Users.ToList();
        }

        public User FindByID(int id)
        {
            return db.Users.Find(id);
        }

        public void Add(User obj)
        {
        }

        public void Update(User obj)
        {
            try
            {
                db.Users.Update(obj);
                db.SaveChanges();
                logger.Information("+BookingRep : Booking {Number} was updated at Bookings", obj.Userid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+BookingRep : Error trying to update booking at Bookings");
            }
        }

        public void DeleteAll()
        {
            try
            {
                List<User> allBookings = FindAll();
                db.Users.RemoveRange(allBookings);
                db.SaveChanges();
                logger.Information("+BookingRep : All bookings were deleted from Bookings");
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+BookingRep : Error trying to delete all bookings from Bookings");
            }
        }

        public void DeleteByID(int id)
        {
        }

        public User GetUserByLP(string login, string password)
        {
            IQueryable<User> users = db.Users.Where(needed => needed.Login.Equals(login) &&
                                                                   needed.Password.Equals(password));
            if (users != null)
            {
                return users.First();
            }
            return null;
        }

        public int[] GetBookToursByID(int id)
        {
            return FindByID(id).Toursid;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
