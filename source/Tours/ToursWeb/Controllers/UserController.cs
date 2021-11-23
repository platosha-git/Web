using System.Collections.Generic;
using System.Linq;
using ToursWeb.ModelsDB;
using ToursWeb.ModelsBL;
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
        
        public List<UserBL> GetAllUsers()
        {
            return _userRepository.FindAll();
        }
        
        public UserBL GetAllUserInfo(int userID)
        {
            return _userRepository.FindByID(userID);
        }

        public UserBL GetUserByLP(string login, string password)
        {
            return _userRepository.FindUserByLP(login, password);
        }

        public List<int> GetBookedTours(int id)
        {
            return _userRepository.FindBookedTours(id);
        }

        public ExitCode BookTour(int userID, int tourID)
        {
            UserBL user = _userRepository.FindByID(userID);
            List<int> oldTours = user.Toursid;
            List<int> newTours = new List<int>();
            int size = oldTours.Count;
            
            if (size > 0)
            {
                foreach (int tour in oldTours)
                {
                    newTours.Add(tour);
                }
            }
            newTours.Add(tourID);

            return _userRepository.UpdateTours(user, newTours);
        }

        public ExitCode CancelTour(int userID, int tourID)
        {
            UserBL user = _userRepository.FindByID(userID);
            List<int> oldTours = user.Toursid;
            List<int> newTours = new List<int>();
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