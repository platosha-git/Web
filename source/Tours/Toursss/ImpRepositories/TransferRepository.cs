using System;
using System.Collections.Generic;
using System.Linq;
using Toursss.Repositories;
using Serilog.Core;

namespace Toursss.ImpRepositories
{
    public class TransferRepository : ITransferRepository, IDisposable
    {
        private readonly ToursContext db;
        private readonly Logger logger;

        public TransferRepository(ToursContext createDB, Logger logDB)
        {
            db = createDB;
            logger = logDB;
        }

        public List<Transfer> FindAll()
        {
            return db.Transfers.ToList();
        }

        public Transfer FindByID(int id)
        {
            return db.Transfers.Find(id);
        }

        public void Add(Transfer obj)
        {
            try
            {
                obj.Transferid = db.Transfers.Count() + 1;
                db.Transfers.Add(obj);
                db.SaveChanges();
                logger.Information("+TransferRep : Transfer {Number} was added to Transfers", obj.Transferid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TransferRep : Error trying to add transfer to Transfers");
            }
        }

        public void Update(Transfer obj)
        {
            try
            {
                db.Transfers.Update(obj);
                db.SaveChanges();
                logger.Information("+TransferRep : Transfer {Number} was updated at Transfers", obj.Transferid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TransferRep : Error trying to update transfer at Transfers");
            }
        }

        public void DeleteAll()
        {
            try
            {
                List<Transfer> allTransfers = FindAll();
                db.Transfers.RemoveRange(allTransfers);
                db.SaveChanges();
                logger.Information("+TransferRep : All transfers were deleted from Transfer");
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TransferRep : Error trying to delete all transfers from Transfer");
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Transfer transfer = FindByID(id);
                db.Transfers.Remove(transfer);
                db.SaveChanges();
                logger.Information("+TransferRep : Transfer {Number} was deleted from Transfers", transfer.Transferid);
            }
            catch (Exception err)
            {
                logger.Error(err.Message, "+TransferRep : Error trying to delete transfer from Transfer");
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}