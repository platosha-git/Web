using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class UsersRepository : IUsersRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(ToursContext createDB, ILogger<UsersRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        public List<User> FindAll()
        {
            return _db.Users.ToList();
        }

        public User FindByID(int id)
        {
            return _db.Users.Find(id);
        }

        public void Add(User obj)
        {
        }

        public void Update(User obj)
        {
            try
            {
                _db.Users.Update(obj);
                _db.SaveChanges();
                _logger.LogInformation("+BookingRep : Booking {Number} was updated at Bookings", obj.Userid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+BookingRep : Error trying to update booking at Bookings");
            }
        }

        public void DeleteByID(int id)
        {
        }

        public User GetUserByLP(string login, string password)
        {
            IQueryable<User> users = _db.Users.Where(needed => needed.Login.Equals(login) &&
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
            _db.Dispose();
        }
    }
}
