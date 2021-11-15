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
        public int? Planeticket { get; set; }
        public int? Trainticket { get; set; }
        public int? Busticket { get; set; }

        public virtual Busticket BusticketNavigation { get; set; }
        public virtual Planeticket PlaneticketNavigation { get; set; }
        public virtual Trainticket TrainticketNavigation { get; set; }
        public virtual ICollection<Tour> Tours { get; set; }
    }
}
