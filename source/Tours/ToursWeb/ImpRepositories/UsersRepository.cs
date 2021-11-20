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

        public ExitCode Add(User obj)
        {
            return ExitCode.Success;
        }

        public void Update(User obj)
        {
            try
            {
                _db.Users.Update(obj);
                _db.SaveChanges();
                _logger.LogInformation("+UsersRep : User {Number} was updated at Users", obj.Userid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+UsersRep : Error trying to update user at Users");
            }
        }

        public void DeleteByID(int id)
        {
        }

        public User FindUserByLP(string login, string password)
        {
            IQueryable<User> users = _db.Users.Where(needed => needed.Login.Equals(login) &&
                                                                   needed.Password.Equals(password));
            if (users != null)
            {
                return users.First();
            }
            return null;
        }
        
        public int[] FindBookedTours(int id)
        {
            return FindByID(id).Toursid;
        }
        
        public bool UpdateTours(User obj, int[] toursID)
        {
            try
            {
                obj.Toursid = toursID;
                _db.Users.Update(obj);
                _db.SaveChanges();
                _logger.LogInformation("+UsersRep : Tours user {Number} was updated at Users", obj.Userid);
                return true;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+UsersRep : Error trying to update tours at Users");
                return false;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
