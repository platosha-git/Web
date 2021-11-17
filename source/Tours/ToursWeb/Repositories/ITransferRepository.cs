using System;
using System.Collections.Generic;
using ToursWeb.ModelsDB;

namespace ToursWeb.Repositories
{
    public interface ITransferRepository : CrudRepository<Transfer, int>
    {
        List<Transfer> FindTransferByCityFrom(string city);
        List<Transfer> FindTransferByCityTo(string city);
        List<Transfer> FindTransferByDate(DateTime date);
    }

}
