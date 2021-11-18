using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;

namespace ToursWeb.Repositories
{
    public interface ITransferRepository : CrudRepository<Transfer, int>
    {
        List<Transfer> FindTransferByType(string type);
        List<Transfer> FindTransfersByCity(string cityFrom);
        List<Transfer> FindTransfersByDate(DateTime date);
    }

}
