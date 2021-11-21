﻿using ToursWeb.ModelsDB;
namespace ToursWeb.Repositories
{
    public interface IUsersRepository : CrudRepository<User, int>
    {
        User FindUserByLP(string login, string password);
        int[] FindBookedTours(int id);
        ExitCode UpdateTours(User obj, int[] toursID);
    }
}
