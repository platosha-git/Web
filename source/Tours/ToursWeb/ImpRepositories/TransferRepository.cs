using System;
using System.Collections.Generic;
using System.Linq;
using ToursWeb.Repositories;
using Microsoft.Extensions.Logging;
using ToursWeb.ModelsDB;

namespace ToursWeb.ImpRepositories
{
    public class TransferRepository : ITransferRepository, IDisposable
    {
        private readonly ToursContext _db;
        private readonly ILogger<TransferRepository> _logger;

        public TransferRepository(ToursContext createDB, ILogger<TransferRepository> logDB)
        {
            _db = createDB;
            _logger = logDB;
        }

        public List<Transfer> FindAll()
        {
            return _db.Transfers.ToList();
        }

        public Transfer FindByID(int id)
        {
            return _db.Transfers.Find(id);
        }

        public ExitCode Add(Transfer obj)
        {
            try
            {
                obj.Transferid = _db.Transfers.Count() + 1;
                _db.Transfers.Add(obj);
                _db.SaveChanges();
                _logger.LogInformation("+TransferRep : Transfer {Number} was added to Transfers", obj.Transferid);
                return ExitCode.Success;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+TransferRep : Constraint violation when trying to add transfer to Transfers");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TransferRep : Error trying to add transfer to Transfers");
                return ExitCode.Error;
            }
        }

        public ExitCode Update(Transfer obj)
        {
            try
            {
                _db.Transfers.Update(obj);
                _db.SaveChanges();
                _logger.LogInformation("+TransferRep : Transfer {Number} was updated at Transfers", obj.Transferid);
                return ExitCode.Success;

            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException err)
            {
                _logger.LogError(err, "+TransferRep : Constraint violation when trying to update transfer at Transfers");
                return ExitCode.Constraint;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TransferRep : Error trying to update transfer at Transfers");
                return ExitCode.Error;
            }
        }

        public ExitCode DeleteByID(int id)
        {
            try
            {
                Transfer transfer = FindByID(id);
                _db.Transfers.Remove(transfer);
                _db.SaveChanges();
                _logger.LogInformation("+TransferRep : Transfer {Number} was deleted from Transfers", transfer.Transferid);
                return ExitCode.Success;
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TransferRep : Error trying to delete transfer from Transfer");
                return ExitCode.Error;
            }
        }

        public List<Transfer> FindTransferByType(string type)
        {
            IQueryable<Transfer> transfers = _db.Transfers.Where(needed => needed.Type.Equals(type));
            return transfers.ToList();
        }

        public List<Transfer> FindTransfersByCity(string cityFrom)
        {
            IQueryable<Transfer> transfers = _db.Transfers.Where(needed => needed.Cityfrom.Equals(cityFrom));
            return transfers.ToList();
        }

        public List<Transfer> FindTransfersByDate(DateTime date)
        {
            DateTime dateBegin = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
            DateTime dateEnd = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            
            IQueryable<Transfer> transfers = _db.Transfers.Where(needed => needed.Departuretime >= dateBegin &&
                                                                           needed.Departuretime <= dateEnd);
            return transfers.ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}