
namespace Toursss.Repositories
{
    public interface IUsersRepository : CrudRepository<User, int>
    {
        User GetUserByLP(string login, string password);
        int[] GetBookToursByID(int id);
    }
}
