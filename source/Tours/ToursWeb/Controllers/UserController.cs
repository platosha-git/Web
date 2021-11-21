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

        public ExitCode BookTour(int userID, int tourID)
        {
            User user = _userRepository.FindByID(userID);
            int[] oldTours = user.Toursid;
            int size = oldTours.Length;
            int[] newTours;
            
            if (size == 0)
            {
                newTours = new int[1];
                newTours[0] = tourID;
            }
            else 
            {
                newTours = new int[size + 1];
                for (int i = 0; i < size; i++)
                {
                    newTours[i] = oldTours[i];
                }

                newTours[size] = tourID;
            }

            return _userRepository.UpdateTours(user, newTours);
        }

        public ExitCode CancelTour(int userID, int tourID)
        {
            User user = _userRepository.FindByID(userID);
            int[] oldTours = user.Toursid;
            int[] newTours = new int[0];

            if (oldTours != null)
            {
                int size = oldTours.Length;
                if (size > 1)
                {
                    newTours = new int[size - 1];

                    int i = 0, j = 0;
                    while (i < size)
                    {
                        int curTour = oldTours[i];
                        if (curTour == tourID)
                        {
                            i++;
                            continue;
                        }

                        newTours[j] = oldTours[i];
                        i++;
                        j++;
                    }
                }
            }

            return _userRepository.UpdateTours(user, newTours);
        }
    }
}