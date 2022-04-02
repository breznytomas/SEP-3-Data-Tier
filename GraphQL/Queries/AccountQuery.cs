using System.Threading.Tasks;
using Database_EFC.Repositories;
using Entity.ModelData;
using HotChocolate;

namespace CarSharing_Database_GraphQL.Queries
{
    // Queries for Accounts
    public partial class Query
    {
        [GraphQLDescription("Get an account by username.")]
        public async Task<Account> GetAccount([Service] IAccountRepo accountRepo, string username)
        {
            return await accountRepo.GetAsync(username);
        }
    }
}