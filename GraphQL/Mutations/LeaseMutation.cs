using System.Threading.Tasks;
using CarSharing_Database_GraphQL.Mutations.Records.LeaseRecords;
using Database_EFC.Repositories;
using Entity.ModelData;
using HotChocolate;

namespace CarSharing_Database_GraphQL.Mutations
{
    public partial class Mutation
    {
        [GraphQLDescription("Add a leases and associate it with a listing and a customer.")]
        public async Task<Lease> AddLease([Service] ILeaseRepo leaseRepo, AddLeaseInput input)
        {
            var lease = new Lease
            {
                LeasedFrom = input.LeasedFrom,
                LeasedTo = input.LeasedTo,
                Canceled = false,
                TotalPrice = input.TotalPrice,
                Listing = new Listing
                {
                    Id = input.Listing.Id
                },
                Customer = new Customer
                {
                    Cpr = input.Customer.Cpr
                }
            };
            return await leaseRepo.AddAsync(lease);
        }
        
        [GraphQLDescription("Update a lease by its id.")]
        public async Task<Lease> UpdateLease([Service] ILeaseRepo leaseRepo, UpdateLeaseInput input)
        {
            var lease = new Lease
            {
                Id = input.Id,
                LeasedFrom = input.LeasedFrom,
                LeasedTo = input.LeasedTo,
                Canceled = input.IsCanceled,
                TotalPrice = input.TotalPrice,
                Listing = new Listing
                {
                    Id = input.Listing.Id
                },
                Customer = new Customer
                {
                    Cpr = input.Customer.Cpr
                }
            };
            return await leaseRepo.UpdateAsync(lease);
        }
        
        [GraphQLDescription("Remove a lease by its id.")]
        public async Task<bool> RemoveLease([Service] ILeaseRepo leaseRepo, int id)
        {
            return await leaseRepo.RemoveAsync(id);
        }
    }
}