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

        public bool BookTour(int userID, int tourID)
        {
            User user = _userRepository.FindByID(userID);
            int[] oldTours = user.Toursid;
            int size = oldTours.Length;
                
            int[] newTours = new int[size + 1];
            for (int i = 0; i < size; i++)
            {
                newTours[i] = oldTours[i];
            }
            newTours[size] = tourID;
            
            return _userRepository.UpdateTours(user, newTours);
        }
    }
}