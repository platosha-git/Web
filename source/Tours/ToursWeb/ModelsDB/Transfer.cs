using System;
using System.Collections.Generic;

#nullable disable

namespace ToursWeb.ModelsDB
{
    public partial class Transfer
    {
        public Transfer()
        {
            Tours = new HashSet<Tour>();
        }

        public int Transferid { get; set; }
        public string Type { get; set; }
        public string Cityfrom { get; set; }
        public string Cityto { get; set; }
        public DateTime? Departuretime { get; set; }
        public int? Cost { get; set; }

        public virtual ICollection<Tour> Tours { get; set; }
    }
}
