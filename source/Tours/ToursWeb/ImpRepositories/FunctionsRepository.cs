using System;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class FunctionsRepository : IFunctionsRepository, IDisposable
    {
        private readonly ToursContext _db;

        public FunctionsRepository(ToursContext createDB)
        {
            _db = createDB;
        }

        public FullUserTour GetFullTour(int TID)
        {
            IQueryable<FullUserTour> tour = _db.fulltour(TID);
            return tour.First();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
