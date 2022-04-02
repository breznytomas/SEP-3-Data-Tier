using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Database_EFC.Persistence;
using Entity.ModelData;
using Logger.Log;
using Microsoft.EntityFrameworkCore;

namespace Database_EFC.Repositories.Impl
{
    public class LeaseRepo : ILeaseRepo
    {
        private readonly CarSharingDbContext _dbContext;

        public LeaseRepo(CarSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Lease> AddAsync(Lease lease)
        {  
            Log.AddLog($"|Repositories/LeaseRepo.AddAsync| : Request : {JsonSerializer.Serialize(lease)}");
            var added = await _dbContext.Leases.AddAsync(lease);
            _dbContext.Attach(lease.Listing);
            _dbContext.Attach(lease.Customer);
            await _dbContext.SaveChangesAsync();
            return added.Entity; 
        }

        public async Task<Lease> GetAsync(int id)
        {
            try
            {
                Log.AddLog($"|Repositories/LeaseRepo.GetAsync| : Request :  Id:{id}");
                return await _dbContext.Leases
                    .IgnoreQueryFilters()
                    .Include(lease => lease.Listing)
                    .Include(lease => lease.Customer)
                    .FirstAsync(lease => lease.Id == id);
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/LeaseRepo.GetAsync| : Error : {e.Message}");
                throw new Exception($"Did not find the lease with id of {id}");
            }
        }

        public async Task<IList<Lease>> GetByListingAsync(int listingId)
        {
            try
            {
                Log.AddLog($"|Repositories/LeaseRepo.GetByListingAsync| : Request :  ListingId:{listingId}");
                return await _dbContext.Leases
                    .IgnoreQueryFilters()
                    .Include(lease => lease.Customer)
                    .Include(lease => lease.Listing)
                    .ThenInclude(listing => listing.Vehicle)
                    .Where(lease => lease.Listing.Id == listingId)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/LeaseRepo.GetByListingAsync| : Error : {e.Message}");
                throw new Exception($"Cannot retrieve the leases for the listing with id '{listingId}'");
            }
        }

        public async Task<IList<Lease>> GetByCustomerAsync(string cpr)
        {
            try
            {
                Log.AddLog($"|Repositories/LeaseRepo.GetByCustomerAsync| : Request :  Cpr:{cpr}");
                return await _dbContext.Leases
                    .IgnoreQueryFilters()
                    .Include(lease => lease.Customer)
                    .Include(lease => lease.Listing)
                    .ThenInclude(listing => listing.Vehicle)
                    .ThenInclude(vehicle => vehicle.Owner)
                    .Where(lease => lease.Customer.Cpr.Equals(cpr))
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/LeaseRepo.GetByCustomerAsync| : Error : {e.Message}");
                throw new Exception($"Cannot retrieve the leases for the customer with cpr '{cpr}'");
            }

        }

        public async Task<Lease> UpdateAsync(Lease lease)
        {
            try
            {
                _dbContext.Update(lease);
                _dbContext.Entry(lease.Customer).State = EntityState.Unchanged;
                _dbContext.Entry(lease.Listing).State = EntityState.Unchanged;
                await _dbContext.SaveChangesAsync();
                Log.AddLog($"|Repositories/LeaseRepo.UpdateAsync| : Reply : {JsonSerializer.Serialize(lease)}");
                return lease;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/LeaseRepo.UpdateAsync| : Error : {e.Message}");
                throw new Exception($"Did not find lease with id of {lease.Id}");
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var toRemove = await _dbContext.Leases.FirstOrDefaultAsync(l => l.Id == id);
            if (toRemove == null) return false;
            try
            {
                Log.AddLog($"|Repositories/LeaseRepo.RemoveAsync| : Request : Id:{id}");
                _dbContext.Leases.Remove(toRemove);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/LeaseRepo.RemoveAsync| : Error : {e.Message}");
                throw new Exception($"Cannot remove the lease with id of {id}");
            }
        }
    }
}