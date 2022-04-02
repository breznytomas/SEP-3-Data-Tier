using System;
using System.Text.Json;
using System.Threading.Tasks;
using Database_EFC.Persistence;
using Entity.ModelData;
using Logger.Log;
using Microsoft.EntityFrameworkCore;

namespace Database_EFC.Repositories.Impl
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly CarSharingDbContext _dbContext;

        public CustomerRepo(CarSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Customer> AddAsync(Customer customer)
        {
            Log.AddLog($"|Repositories/CustomerRepo.AddAsync| : Request : {JsonSerializer.Serialize(customer)}");
            var added = await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<Customer> GetAsync(string cpr)
        {
            try
            {
                Log.AddLog($"|Repositories/CustomerRepo.GetAsync| : Request :  Cpr:{cpr}");
                Customer customer = await _dbContext.Customers
                    .FirstAsync(customer => customer.Cpr.Equals(cpr));
                return customer;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/CustomerRepo.GetAsync| : Error : {e.Message}");
                throw new Exception($"Did not find the customer with cpr '{cpr}'");
            }
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            try
            {
                _dbContext.Update(customer);
                _dbContext.Entry(customer.AccessLevel).State = EntityState.Unchanged;
                await _dbContext.SaveChangesAsync();
                Log.AddLog($"|Repositories/CustomerRepo.UpdateAsync| : Reply : {JsonSerializer.Serialize(customer)}");
                return customer;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/CustomerRepo.UpdateAsync| : Error : {e.Message}");
                throw new Exception($"Did not find the customer with cpr {customer.Cpr}");
            }
        }

        public async Task<bool> RemoveAsync(string cpr)
        {
            var toRemove = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Cpr.Equals(cpr));
            if (toRemove == null) return false;
            try
            {
                Log.AddLog($"|Repositories/CustomerRepo.RemoveAsync| : Request : Cpr:{cpr}");
                _dbContext.Customers.Remove(toRemove);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/CustomerRepo.RemoveAsync| : Error : {e.Message}");
                throw new Exception($"Cannot remove  the customer with cpr '{cpr}'");
            }
        }
    }
}