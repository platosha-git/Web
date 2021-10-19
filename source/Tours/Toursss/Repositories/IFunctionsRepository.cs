using System.Collections.Generic;
using Toursss.ModelsDB;

namespace Toursss.Repositories
{
    public interface IFunctionsRepository
    {
        FullUserTour GetFullTour(int TID);
    }
}
