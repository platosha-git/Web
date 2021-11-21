using System;
using ToursWeb.ModelsDB;

namespace ToursAPI.ModelsDTO
{
    public class TourUserDTO
    {
        public int Food { get; set; }
        public int Hotel { get; set; }
        public int Transfer { get; set; }
        public int Cost { get; set; }
        public DateTime Datebegin { get; set; }
        public DateTime Dateend { get; set; }

        public TourUserDTO() {}
        
        public Tour GetTour(int tourID = 0)
        {
            Tour tour = new Tour ()
            {
                Tourid = tourID,
                Food = Food,
                Hotel = Hotel,
                Transfer = Transfer,
                Cost = Cost,
                Datebegin = Datebegin,
                Dateend = Dateend
            };
            
            return tour;
        }
    }

    public class TourDTO : TourUserDTO
    {
        public int Tourid { get; set; }

        public TourDTO() {}

        public TourDTO(Tour tour)
        {
            Tourid = tour.Tourid;
            Food = tour.Food;
            Hotel = tour.Hotel;
            Transfer = tour.Transfer;
            Cost = tour.Cost;
            Datebegin = tour.Datebegin;
            Dateend = tour.Dateend;
        }
    }
}
