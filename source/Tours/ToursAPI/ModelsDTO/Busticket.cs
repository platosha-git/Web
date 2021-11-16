using System;
using ToursWeb.ModelsDB;

namespace ToursAPI.ModelsDTO
{
    public class BusticketDTO
    {
        public int Bustid { get; set; }
        public int? Bus { get; set; }
        public int? Seat { get; set; }
        public string Cityfrom { get; set; }
        public string Cityto { get; set; }
        public DateTime? Departuretime { get; set; }
        public DateTime? Arrivaltime { get; set; }
        public bool? Luggage { get; set; }
        public int? Cost { get; set; }

        public BusticketDTO()
        {
        }

        public BusticketDTO(Busticket bus)
        {
            Bustid = bus.Bustid;
            Bus = bus.Bus;
            Seat = bus.Seat;
            Cityfrom = bus.Cityfrom;
            Cityto = bus.Cityto;
            Departuretime = bus.Departuretime;
            Arrivaltime = bus.Arrivaltime;
            Luggage = bus.Luggage;
            Cost = bus.Cost;
        }

        public Busticket GetBusticket()
        {
            Busticket bus = new Busticket ()
            {
                Bustid = Bustid,
                Bus = Bus,
                Seat = Seat,
                Cityfrom = Cityfrom,
                Cityto = Cityto,
                Departuretime = Departuretime,
                Arrivaltime = Arrivaltime,
                Luggage = Luggage,
                Cost = Cost
            };
            return bus;
        }
        
        public bool AreEqual(Busticket bus)
        {
            if (Bustid == bus.Bustid &&
                Bus == bus.Bus &&
                Seat == bus.Seat &&
                Cityfrom == bus.Cityfrom &&
                Cityto == bus.Cityto &&
                Departuretime == bus.Departuretime &&
                Arrivaltime == bus.Arrivaltime &&
                Luggage == bus.Luggage &&
                Cost == bus.Cost)
                return true;
            return false;
        }
    }
}