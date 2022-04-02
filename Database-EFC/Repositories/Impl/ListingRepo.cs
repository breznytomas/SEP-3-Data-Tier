using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Database_EFC.Persistence;
using Entity.ModelData;
using Logger.Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Database_EFC.Repositories.Impl
{
    public class ListingRepo : IListingRepo
    {
        private readonly CarSharingDbContext _dbContext;

        public ListingRepo(CarSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Listing> AddAsync(Listing listing)
        {
            Log.AddLog($"|Repositories/ListingRepo.AddAsync| : Request : {JsonSerializer.Serialize(listing)}");
            var added = await _dbContext.Listings.AddAsync(listing);
            _dbContext.Attach(listing.Vehicle);
            await _dbContext.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<IList<Listing>> GetAsync(string location, DateTime dateFrom, DateTime dateTo)
        {
            Log.AddLog(
                $"|Repositories/ListingRepo.GetAsync| : Request :  Location:{location}, DateFrom:{dateFrom}, DateTo:{dateTo}");
            return await _dbContext.Listings
                .Include(listing => listing.Vehicle)
                .Where(l =>
                    l.Location.Equals(location)
                    && l.DateFrom < dateFrom
                    && l.DateTo > dateTo)
                .ToListAsync();
        }

        public async Task<IList<Listing>> GetByVehicleAsync(string licenseNo)
        {
            Log.AddLog(
                $"|Repositories/ListingRepo.GetByVehicleAsync| : Request :  LicenseNo:{licenseNo}");
            return await _dbContext.Listings
                .Include(listing => listing.Vehicle)
                .Where(l => l.Vehicle.LicenseNo.Equals(licenseNo))
                .ToListAsync();
        }

        public async Task<Listing> GetByIdAsync(int id)
        {
            try
            {
                Log.AddLog(
                    $"|Repositories/ListingRepo.GetByIdAsync| : Request :  Id:{id}");
                return await _dbContext.Listings
                    .Include(listing => listing.Vehicle)
                    .ThenInclude(v => v.Owner)
                    .FirstAsync(l => l.Id == id);
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/ListingRepo.GetByIdAsync| : Error : {e.Message}");
                throw new Exception($"Did not find the listing with id of {id}");
            }
        }

        public Task<Listing> GetAsync(string location, DateInterval dateInterval)
        {
            throw new NotImplementedException();
        }

        public async Task<Listing> UpdateAsync(Listing listing)
        {
            try
            {
                _dbContext.Update(listing);
                _dbContext.Entry(listing.Vehicle).State = EntityState.Unchanged;
                _dbContext.Entry(listing).Property(l => l.ListedDate).IsModified = false;
                await _dbContext.SaveChangesAsync();
                Log.AddLog($"|Repositories/ListingRepo.UpdateAsync| : Reply : {JsonSerializer.Serialize(listing)}");
                return listing;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/ListingRepo.UpdateAsync| : Error : {e.Message}");
                throw new Exception($"Did not find listing with Id #{listing.Id}");
            }
        }


        public async Task<bool> RemoveAsync(int id)
        {
            var toRemove = await _dbContext.Listings.FirstOrDefaultAsync(l => l.Id == id);
            if (toRemove == null) return false;

            try
            {
                Log.AddLog($"|Repositories/ListingRepo.RemoveAsync| : Request : Id:{id}");
                _dbContext.Listings.Remove(toRemove);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/ListingRepo.RemoveAsync| : Error : {e.Message}");
                throw new Exception($"Cannot remove the listing with Id #{id}");
            }
        }
    }
}