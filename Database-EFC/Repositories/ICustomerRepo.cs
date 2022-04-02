using System.Threading.Tasks;
using Entity.ModelData;

namespace Database_EFC.Repositories
{
    public interface ICustomerRepo
    {
        Task<Customer> AddAsync(Customer customer);
        Task<Customer> GetAsync(string cpr);
        Task<Customer> UpdateAsync(Customer customer);
        Task<bool> RemoveAsync(string cpr);
    }
}