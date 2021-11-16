using System;
using ToursWeb.ModelsDB;

namespace ToursAPI.ModelsDTO
{
    public class TransferDTO
    {
        public int Transferid { get; set; }
        public int? Planeticket { get; set; }
        public int? Trainticket { get; set; }
        public int? Busticket { get; set; }

        public TransferDTO()
        {
        }

        public TransferDTO(Transfer transfer)
        {
            Transferid = transfer.Transferid;
            Planeticket = transfer.Planeticket;
            Trainticket = transfer.Trainticket;
            Busticket = transfer.Busticket;
        }
        
        public Transfer GetTransfer()
        {
            Transfer transfer = new Transfer ()
            {
                Transferid = Transferid,
                Planeticket = Planeticket,
                Trainticket = Trainticket,
                Busticket = Busticket
            };
            return transfer;
        }
        
        public bool AreEqual(Transfer transfer)
        {
            if (Transferid == transfer.Transferid &&
                Planeticket == transfer.Planeticket &&
                Trainticket == transfer.Trainticket &&
                Busticket == transfer.Busticket)
                return true;
            return false;
        }
    }
}