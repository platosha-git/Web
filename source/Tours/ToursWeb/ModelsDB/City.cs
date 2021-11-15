using System.Collections.Generic;

#nullable disable

namespace ToursWeb.ModelsDB
{
    public partial class City
    {
        public City()
        {
            BusticketCityfromNavigations = new HashSet<Busticket>();
            BusticketCitytoNavigations = new HashSet<Busticket>();
            Hotels = new HashSet<Hotel>();
            PlaneticketCityfromNavigations = new HashSet<Planeticket>();
            PlaneticketCitytoNavigations = new HashSet<Planeticket>();
            TrainticketCityfromNavigations = new HashSet<Trainticket>();
            TrainticketCitytoNavigations = new HashSet<Trainticket>();
        }

        public int Cityid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Busticket> BusticketCityfromNavigations { get; set; }
        public virtual ICollection<Busticket> BusticketCitytoNavigations { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Planeticket> PlaneticketCityfromNavigations { get; set; }
        public virtual ICollection<Planeticket> PlaneticketCitytoNavigations { get; set; }
        public virtual ICollection<Trainticket> TrainticketCityfromNavigations { get; set; }
        public virtual ICollection<Trainticket> TrainticketCitytoNavigations { get; set; }
    }
}
