using System;
using ToursWeb.ModelsDB;

namespace ToursWeb.ModelsBL
{
    public enum TType
    {
        Bus,
        Train,
        Plane
    }
    
    public class TransferBL
    {
        public int Transferid { get; set; }
        public TType Type { get; set; }
        public string Cityfrom { get; set; }
        public DateTime? Departuretime { get; set; }
        public int? Cost { get; set; }

        public TransferBL() { }

        public TransferBL(Transfer transfer)
        {
            Transferid = transfer.Transferid;
            Type = (TType) Enum.Parse(typeof(TType), transfer.Type, true);
            Cityfrom = transfer.Cityfrom;
            Departuretime = transfer.Departuretime;
            Cost = transfer.Cost;
        }

        public Transfer GetTransfer()
        {
            Transfer transfer = new Transfer()
            {
                Transferid = Transferid,
                Type = Type.ToString(),
                Cityfrom = Cityfrom,
                Departuretime = Departuretime,
                Cost = Cost
            };

            return transfer;
        }
    }
}