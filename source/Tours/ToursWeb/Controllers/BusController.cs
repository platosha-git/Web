using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class BusController
    {
        private readonly IBusRepository _busRepository;

        public BusController(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        public List<Busticket> GetAllBuses()
        {
            return _busRepository.FindAll();
        }

        public Busticket GetBusByID(int busID)
        {
            return _busRepository.FindByID(busID);
        }

        public List<Busticket> GetBusesByCityFrom(string city)
        {
            return _busRepository.FindBusesByCityFrom(city);
        }

        public List<Busticket> GetBusesByCityTo(string city)
        {
            return _busRepository.FindBusesByCityTo(city);
        }

        public List<Busticket> GetBusesByDate(DateTime date)
        {
            return _busRepository.FindBusesByDate(date);
        }
        
        public void AddBus(Busticket nbus)
        {
            _busRepository.Add(nbus);
        }
        
        public void UpdateBus(Busticket nbus)
        {
            _busRepository.Update(nbus);
        }
        
        public void DeleteBusByID(int busID)
        {
            _busRepository.DeleteByID(busID);
        }
    }
}
