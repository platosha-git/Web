﻿using System;
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

        public void Add(Transfer obj)
        {
            try
            {
                obj.Transferid = _db.Transfers.Count() + 1;
                _db.Transfers.Add(obj);
                _db.SaveChanges();
                _logger.LogInformation("+TransferRep : Transfer {Number} was added to Transfers", obj.Transferid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TransferRep : Error trying to add transfer to Transfers");
            }
        }

        public void Update(Transfer obj)
        {
            try
            {
                _db.Transfers.Update(obj);
                _db.SaveChanges();
                _logger.LogInformation("+TransferRep : Transfer {Number} was updated at Transfers", obj.Transferid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TransferRep : Error trying to update transfer at Transfers");
            }
        }

        public void DeleteByID(int id)
        {
            try
            {
                Transfer transfer = FindByID(id);
                _db.Transfers.Remove(transfer);
                _db.SaveChanges();
                _logger.LogInformation("+TransferRep : Transfer {Number} was deleted from Transfers", transfer.Transferid);
            }
            catch (Exception err)
            {
                _logger.LogError(err, "+TransferRep : Error trying to delete transfer from Transfer");
            }
        }
        
        public List<Transfer> FindTransferByCityFrom(string city)
        {
            IQueryable<Transfer> transfers = _db.Transfers.Where(needed => needed.Cityfrom.Equals(city));
            return transfers.ToList();
        }

        public List<Transfer> FindTransferByCityTo(string city)
        {
            IQueryable<Transfer> transfers = _db.Transfers.Where(needed => needed.Cityto.Equals(city));
            return transfers.ToList();
        }

        public List<Transfer> FindTransferByDate(DateTime date)
        {
            IQueryable<Transfer> transfers = _db.Transfers.Where(needed => needed.Departuretime == date);
            return transfers.ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}