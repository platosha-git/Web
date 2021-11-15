using System;
using ToursWeb.ModelsDB;

namespace ToursAPI.ModelsDTO
{
    public class FullUserTourDTO
    {
        public int tourid { get; set; }
        public string city { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string category { get; set; }
        public int transfer { get; set; }
        public int cost { get; set; }
        public DateTime datebegin { get; set; }
        public DateTime dateend { get; set; }
        
        public FullUserTourDTO()
        {
        }
        
        public FullUserTourDTO(FullUserTour tour)
        {
            tourid = tour.tourid;
            city = tour.city; 
            name = tour.name;
            type = tour.type; 
            category = tour.category; 
            transfer = tour.transfer; 
            cost = tour.cost; 
            datebegin = tour.datebegin; 
            dateend = tour.dateend;
        }
    }
}