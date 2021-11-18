using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;

namespace ToursWeb.Repositories
{
    public interface ITransferRepository : CrudRepository<Transfer, int>
    {
        List<Transfer> FindTransferByType(string type);
        List<Transfer> FindTransfersByCities(string cityFrom, string cityTo);
        List<Transfer> FindTransfersByDate(DateTime date);
    }

}
