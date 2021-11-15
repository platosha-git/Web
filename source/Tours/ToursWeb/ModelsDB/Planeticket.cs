using System;
using System.Collections.Generic;

#nullable disable

namespace ToursWeb.ModelsDB
{
    public partial class Planeticket
    {
        public Planeticket()
        {
            Transfers = new HashSet<Transfer>();
        }

        public int Planetid { get; set; }
        public int? Plane { get; set; }
        public int? Seat { get; set; }
        public int? Class { get; set; }
        public string Cityfrom { get; set; }
        public string Cityto { get; set; }
        public DateTime? Departuretime { get; set; }
        public bool? Luggage { get; set; }
        public int? Cost { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
