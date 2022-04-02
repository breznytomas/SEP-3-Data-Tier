using System.Threading.Tasks;
using CarSharing_Database_GraphQL.Mutations.Records.AccountRecords;
using Database_EFC.Repositories;
using Entity.ModelData;
using HotChocolate;

namespace CarSharing_Database_GraphQL.Mutations
{
    public partial class Mutation
    {
        [GraphQLDescription("Add an account and associate it with a customer.")]
        public async Task<Account> AddAccount([Service] IAccountRepo accountRepo, AccountInput input)
        {
            var account = new Account
            {
                Username = input.Username,
                Password = input.Password,
                Customer = new Customer
                {
                    Cpr = input.Customer.Cpr
                }
            };
            return await accountRepo.AddAsync(account);
        }
    }
}