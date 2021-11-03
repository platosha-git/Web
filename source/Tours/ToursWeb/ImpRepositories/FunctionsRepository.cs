using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Serilog.Core;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class FunctionsRepository : IFunctionsRepository, IDisposable
    {
        private readonly ToursContext db;
        private readonly Logger logger;

        public FunctionsRepository(ToursContext createDB, Logger logDB)
        {
            db = createDB;
            logger = logDB;
        }

        public FullUserTour GetFullTour(int TID)
        {
            IQueryable<FullUserTour> tour = db.fulltour(TID);
            return tour.First();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
