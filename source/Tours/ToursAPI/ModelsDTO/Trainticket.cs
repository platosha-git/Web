using System;
using ToursWeb.ModelsDB;

namespace ToursAPI.ModelsDTO
{
    public class TrainticketDTO
    {
        public int Traintid { get; set; }
        public int? Train { get; set; }
        public int? Coach { get; set; }
        public int? Seat { get; set; }
        public string Cityfrom { get; set; }
        public string Cityto { get; set; }
        public DateTime? Departuretime { get; set; }
        public DateTime? Arrivaltime { get; set; }
        public bool? Linens { get; set; }
        public int? Cost { get; set; }
        
        public TrainticketDTO()
        {
        }

        public TrainticketDTO(Trainticket train)
        {
            Traintid = train.Traintid;
            Train = train.Train;
            Coach = train.Coach;
            Seat = train.Seat;
            Cityfrom = train.Cityfrom;
            Cityto = train.Cityto;
            Departuretime = train.Departuretime;
            Arrivaltime = train.Arrivaltime;
            Linens = train.Linens;
            Cost = train.Cost;
        }

        public Trainticket GetTrainticket()
        {
            Trainticket train = new Trainticket ()
            {
                Traintid = Traintid,
                Train = Train,
                Coach = Coach,
                Seat = Seat,
                Cityfrom = Cityfrom,
                Cityto = Cityto,
                Departuretime = Departuretime,
                Arrivaltime = Arrivaltime,
                Linens = Linens,
                Cost = Cost
            };
            return train;
        }
        
        public bool AreEqual(Trainticket train)
        {
            if (Traintid == train.Traintid &&
                Train == train.Train &&
                Coach == train.Coach &&
                Seat == train.Seat &&
                Cityfrom == train.Cityfrom &&
                Cityto == train.Cityto &&
                Departuretime == train.Departuretime &&
                Arrivaltime == train.Arrivaltime &&
                Linens == train.Linens &&
                Cost == train.Cost)
                return true;
            return false;
        }
    }
}