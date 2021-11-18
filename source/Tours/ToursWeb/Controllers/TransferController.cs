using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;
using ToursWeb.Repositories;

namespace ToursWeb.Controllers
{
    public class TransferController
    {
        private readonly ITransferRepository _transferRepository;

        public TransferController(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        public List<Transfer> GetAllTransfer()
        {
            return _transferRepository.FindAll();
        }
        
        public Transfer GetTransferByID(int transfID)
        {
            return _transferRepository.FindByID(transfID);
        }
        
        public List<Transfer> GetTransferByType(string type)
        {
            return _transferRepository.FindTransferByType(type);
        }
        
        public List<Transfer> GetTransfersByCity(string cityFrom)
        {
            return _transferRepository.FindTransfersByCity(cityFrom);
        }

        public List<Transfer> GetTransfersByDate(DateTime date)
        {
            return _transferRepository.FindTransfersByDate(date);
        }
        
        public void AddTransfer(Transfer ntran)
        {
            _transferRepository.Add(ntran);
        }
        
        public void UpdateTransfer(Transfer ntran)
        {
            _transferRepository.Update(ntran);
        }
        
        public void DeleteTransferByID(int tranID)
        {
            _transferRepository.DeleteByID(tranID);
        }
    }
}