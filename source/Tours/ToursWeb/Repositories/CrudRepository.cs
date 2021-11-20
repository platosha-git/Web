using System.Collections.Generic;
using ToursWeb.ModelsDB;

namespace ToursWeb.Repositories
{
    public interface CrudRepository<T, ID>
    {
        List<T> FindAll();
        T FindByID(ID id);
        ExitCode Add(T obj);
        void Update(T obj);
        void DeleteByID(ID id);
    }
}
