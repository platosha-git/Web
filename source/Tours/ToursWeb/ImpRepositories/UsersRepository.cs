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
            try
            {
                obj.Userid = _db.Users.Count() + 1;
                _db.Users.Add(obj);
                _db.SaveChanges();
                _logger.LogInformation("+UsersRep : User {Number} was added to Users", obj.Userid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+UsersRep : Constraint violation when trying to add user to Users");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+UsersRep : Error trying to add user to Users");
                return ExitCode.Error;
            }
        }

        public ExitCode Update(User obj)
        {
            try
            {
                _db.Users.Update(obj);
                _db.SaveChanges();
                _logger.LogInformation("+UsersRep : User {Number} was updated at Users", obj.Userid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+UsersRep : Constraint violation when trying to update user to Users");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+UsersRep : Error trying to update user at Users");
                return ExitCode.Error;
            }
        }

        public ExitCode DeleteByID(int id)
        {
            try
            {
                User user = FindByID(id);
                _db.Users.Remove(user);
                _db.SaveChanges();
                _logger.LogInformation("+UsersRep : User {Number} was deleted from Users", id);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+UsersRep : Error trying to delete user {Number} from Users", id);
                return ExitCode.Error;
            }
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
        
        public ExitCode UpdateTours(User obj, int[] toursID)
        {
            try
            {
                obj.Toursid = toursID;
                _db.Users.Update(obj);
                _db.SaveChanges();
                _logger.LogInformation("+UsersRep : Tours user {Number} was updated at Users", obj.Userid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+UsersRep : Constraint violation when trying to update user tours to Users");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+UsersRep : Error trying to update tours at Users");
                return ExitCode.Error;
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
