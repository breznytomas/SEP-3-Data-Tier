using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.ModelData;

namespace Database_EFC.Repositories
{
    public interface ILeaseRepo
    {
        Task<Lease> AddAsync(Lease lease);
        Task<Lease> GetAsync(int id);
        Task<IList<Lease>> GetByListingAsync(int listingId);
        Task<IList<Lease>> GetByCustomerAsync(string cpr);
        Task<Lease> UpdateAsync(Lease lease);
        Task<bool> RemoveAsync(int id);
    }
}