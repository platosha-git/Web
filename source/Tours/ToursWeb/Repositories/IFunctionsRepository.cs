using System.Collections.Generic;
using ToursWeb.ModelsDB;

namespace ToursWeb.Repositories
{
    public interface IFunctionsRepository
    {
        FullUserTour GetFullTour(int TID);
    }
}
