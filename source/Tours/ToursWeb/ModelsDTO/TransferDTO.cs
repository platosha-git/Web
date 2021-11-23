using System;
using ToursWeb.ModelsDB;

namespace ToursWeb.ModelsDTO
{
    public class TransferUserDTO
    {
        public string Type { get; set; }
        public string Cityfrom { get; set; }
        public DateTime? Departuretime { get; set; }
        public int? Cost { get; set; }

        public TransferUserDTO() {}

        public Transfer GetTransfer(int transferID = 0)
        {
            Transfer transfer = new Transfer ()
            {
                Transferid = transferID,
                Type = Type,
                Cityfrom = Cityfrom,
                Departuretime = Departuretime,
                Cost = Cost
            };
            
            return transfer;
        }
    }

    public class TransferDTO : TransferUserDTO
    {
        public int Transferid { get; set; }
        
        public TransferDTO() {}
        
        public TransferDTO(Transfer transfer)
        {
            Transferid = transfer.Transferid;
            Type = transfer.Type;
            Cityfrom = transfer.Cityfrom;
            Departuretime = transfer.Departuretime;
            Cost = transfer.Cost;
        }
    }
}