using System;
using ToursWeb.ModelsDB;

namespace ToursAPI.ModelsDTO
{
    public class TransferDTO
    {
        public int Transferid { get; set; }
        public string Type { get; set; }
        public string Cityfrom { get; set; }
        public string Cityto { get; set; }
        public DateTime? Departuretime { get; set; }
        public int? Cost { get; set; }

        public TransferDTO()
        {
        }

        public TransferDTO(Transfer transfer)
        {
            Transferid = transfer.Transferid;
            Type = transfer.Type;
            Cityfrom = transfer.Cityfrom;
            Cityto = transfer.Cityto;
            Departuretime = transfer.Departuretime;
            Cost = transfer.Cost;
        }
        
        public Transfer GetTransfer()
        {
            Transfer transfer = new Transfer ()
            {
                Transferid = Transferid,
                Type = Type,
                Cityfrom = Cityfrom,
                Cityto = Cityto,
                Departuretime = Departuretime,
                Cost = Cost
            };
            return transfer;
        }
        
        public bool AreEqual(Transfer transfer)
        {
            if (Transferid == transfer.Transferid &&
                Type == transfer.Type &&
                Cityfrom == transfer.Cityfrom &&
                Cityto == transfer.Cityto &&
                Departuretime == transfer.Departuretime &&
                Cost == transfer.Cost)
                return true;
            return false;
        }
    }
}