using System.Collections.Generic;
using System.Threading.Tasks;
using Database_EFC.Repositories;
using Entity.ModelData;
using HotChocolate;

namespace CarSharing_Database_GraphQL.Queries
{
    // Queries for Vehicles
    public partial class Query
    {

        [GraphQLDescription("Get a vehicle by license number.")]
        public async Task<Vehicle> GetVehicle([Service] IVehicleRepo vehicleRepo, string licenseNo)
        {
            return await vehicleRepo.GetAsync(licenseNo);
        }
        
        [GraphQLDescription("Get a list of vehicles by owner's cpr.")]
        public async Task<IList<Vehicle>> GetVehiclesByOwner([Service] IVehicleRepo vehicleRepo, string cpr)
        {
            return await vehicleRepo.GetByOwnerAsync(cpr);
        }
        
        [GraphQLDescription("Get a list of vehicles by approval status.")]
        public async Task<IList<Vehicle>> GetVehiclesByApprovalStatus([Service] IVehicleRepo vehicleRepo, bool isApproved)
        {
            return await vehicleRepo.GetByApprovalStatusAsync(isApproved);
        }
    }
}