using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class UserController
    {
        private readonly IUsersRepository _userRepository;

        public UserController(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public List<User> GetAllUsers()
        {
            return _userRepository.FindAll();
        }
        
        public User GetAllUserInfo(int userID)
        {
            return _userRepository.FindByID(userID);
        }

        public User GetUserByLP(string login, string password)
        {
            return _userRepository.FindUserByLP(login, password);
        }

        public int[] GetBookedTours(int id)
        {
            return _userRepository.FindBookedTours(id);
        }
    }
}