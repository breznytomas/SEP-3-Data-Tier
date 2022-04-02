using System;
using System.Text.Json;
using System.Threading.Tasks;
using Database_EFC.Persistence;
using Entity.ModelData;
using Logger.Log;
using Microsoft.EntityFrameworkCore;

namespace Database_EFC.Repositories.Impl
{
    public class AccountRepo : IAccountRepo
    {
        private readonly CarSharingDbContext _dbContext;

        public AccountRepo(CarSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Account> AddAsync(Account account)
        {
            Log.AddLog($"|Repositories/AccountRepo.AddAsync| : Request : {JsonSerializer.Serialize(account)}");
            var added = await _dbContext.Accounts.AddAsync(account);
            _dbContext.Attach(account.Customer);
            await _dbContext.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<Account> GetAsync(string username)
        {
            try
            {
                Log.AddLog($"|Repositories/AccountRepo.GetAsync| : Request : {username}");
                Account account = await _dbContext.Accounts
                    .Include(a => a.Customer)
                    .FirstAsync(account => account.Username.Equals(username));
                
                return account;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/AccountRepo.GetAsync| : Error : {e.Message}");
                throw new Exception($"Did not find the account with username '{username}'");
            }
        }
    }
}