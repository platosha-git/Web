using System;
using ToursWeb.ModelsDB;

namespace ToursAPI.ModelsDTO
{
    public class PlaneticketDTO
    {
        public int Planetid { get; set; }
        public int? Plane { get; set; }
        public int? Seat { get; set; }
        public int? Class { get; set; }
        public string Cityfrom { get; set; }
        public string Cityto { get; set; }
        public DateTime? Departuretime { get; set; }
        public bool? Luggage { get; set; }
        public int? Cost { get; set; }
        
        public PlaneticketDTO()
        {
        }

        public PlaneticketDTO(Planeticket plane)
        {
            Planetid = plane.Planetid;
            Plane = plane.Plane;
            Seat = plane.Seat;
            Class = plane.Class;
            Cityfrom = plane.Cityfrom;
            Cityto = plane.Cityto;
            Departuretime = plane.Departuretime;
            Luggage = plane.Luggage;
            Cost = plane.Cost;
        }

        public Planeticket GetPlaneticket()
        {
            Planeticket plane = new Planeticket ()
            {
                Planetid = Planetid,
                Plane = Plane,
                Seat = Seat,
                Class = Class,
                Cityfrom = Cityfrom,
                Cityto = Cityto,
                Departuretime = Departuretime,
                Luggage = Luggage,
                Cost = Cost
            };
            return plane;
        }
        
        public bool AreEqual(Planeticket plane)
        {
            if (Planetid == plane.Planetid &&
                Plane == plane.Plane &&
                Seat == plane.Seat &&
                Class == plane.Class &&
                Cityfrom == plane.Cityfrom &&
                Cityto == plane.Cityto &&
                Departuretime == plane.Departuretime &&
                Luggage == plane.Luggage &&
                Cost == plane.Cost)
                return true;
            return false;
        }
    }
}