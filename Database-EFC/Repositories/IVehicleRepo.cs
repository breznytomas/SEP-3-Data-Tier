using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.ModelData;

namespace Database_EFC.Repositories
{
    public interface IVehicleRepo
    {
        Task<Vehicle> AddAsync(Vehicle vehicle);
        Task<Vehicle> GetAsync(string licenseNo);
        Task<List<Vehicle>> GetByOwnerAsync(string cpr);
        Task<List<Vehicle>> GetByApprovalStatusAsync(bool isApproved);
        Task<Vehicle> UpdateAsync(Vehicle vehicle);
        Task<bool> RemoveAsync(string licenseNo);
    }
}