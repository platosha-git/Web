using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class FullUserTourController
    {
        private readonly IFunctionsRepository _funcRepository;

        public FullUserTourController(IFunctionsRepository funcRepository)
        {
            _funcRepository = funcRepository;
        }

        public FullUserTour GetFullTour(int tourID)
        {
            return _funcRepository.GetFullTour(tourID);
        }
    }
}