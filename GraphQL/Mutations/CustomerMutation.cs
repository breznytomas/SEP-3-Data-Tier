using System.Threading.Tasks;
using CarSharing_Database_GraphQL.Mutations.Records.CustomerRecords;
using Database_EFC.Repositories;
using Entity.ModelData;
using HotChocolate;

namespace CarSharing_Database_GraphQL.Mutations
{
    public partial class Mutation
    {
        [GraphQLDescription("Add a customer.")]
        public async Task<Customer> AddCustomer([Service] ICustomerRepo customerRepo, CustomerInput customer)
        {
            return await customerRepo.AddAsync(
                new Customer
                {
                    Cpr = customer.Cpr,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNo = customer.PhoneNo,
                    AccessLevel = 0
                }
            );
        }

        [GraphQLDescription("Update a customer by his cpr.")]
        public async Task<Customer> UpdateCustomer([Service] ICustomerRepo customerRepo, CustomerInput customer)
        {
            return await customerRepo.UpdateAsync(
                new Customer
                {
                    Cpr = customer.Cpr,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNo = customer.PhoneNo,
                    AccessLevel = 0
                }
            );
        }

        [GraphQLDescription("Remove a customer by his cpr.")]
        public async Task<bool> RemoveCustomer([Service] ICustomerRepo customerRepo, string cpr)
        {
            return await customerRepo.RemoveAsync(cpr);
        }
    }
}