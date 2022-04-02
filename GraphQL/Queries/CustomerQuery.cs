using System.Threading.Tasks;
using Database_EFC.Repositories;
using Entity.ModelData;
using HotChocolate;

namespace CarSharing_Database_GraphQL.Queries
{
    // Queries for Customers
    public partial class Query
    {
        [GraphQLDescription("Get a customer by cpr.")]
        public async Task<Customer> GetCustomer([Service] ICustomerRepo customerRepo, string cpr)
        {
            return await customerRepo.GetAsync(cpr);
        }
    }
}