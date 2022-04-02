using System.Threading.Tasks;
using Entity.ModelData;

namespace Database_EFC.Repositories
{
    public interface IAccountRepo
    {
        Task<Account> AddAsync(Account account);
        Task<Account> GetAsync(string username);
    }
}