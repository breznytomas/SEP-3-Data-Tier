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
    public class VehicleRepo : IVehicleRepo
    {
        private readonly CarSharingDbContext _dbContext;

        public VehicleRepo(CarSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Vehicle> AddAsync(Vehicle vehicle)
        {
            //it assumes that the licenseNo is tied to one car and original owner, so it updates only the millage.
            var existing = await _dbContext.Vehicles.IgnoreQueryFilters().FirstOrDefaultAsync(v => v.LicenseNo == vehicle.LicenseNo && v.IsDeleted);
            if (existing != null)
            {
                existing.Mileage = vehicle.Mileage;
                existing.IsDeleted = false;
                Log.AddLog($"|Repositories/VehicleRepo.AddAsync| : Request : Restored - {JsonSerializer.Serialize(vehicle)}");
                _dbContext.Attach(vehicle.Owner);
                await _dbContext.SaveChangesAsync();
                return existing;
            }
            
            Log.AddLog($"|Repositories/VehicleRepo.AddAsync| : Request : {JsonSerializer.Serialize(vehicle)}");
            var added = await _dbContext.Vehicles.AddAsync(vehicle);
            _dbContext.Attach(vehicle.Owner);
            await _dbContext.SaveChangesAsync();
            return added.Entity;
        }
        

        public async Task<Vehicle> GetAsync(string licenseNo)
        {
            try
            {
                Log.AddLog($"|Repositories/VehicleRepo.GetAsync| : Request :  LicenseNo:{licenseNo}");
                Vehicle vehicle = await _dbContext.Vehicles
                    .Include(vehicle => vehicle.Owner)
                    .FirstAsync(vehicle => vehicle.LicenseNo == licenseNo);
                return vehicle;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/VehicleRepo.GetAsync| : Error : {e.Message}");
                throw new Exception($"Did not find the vehicle with licenseNo of {licenseNo}");
            }
        }

        public async Task<List<Vehicle>> GetByOwnerAsync(string cpr)
        {
            try
            {
                Log.AddLog($"|Repositories/VehicleRepo.GetByOwnerAsync| : Request :  Cpr:{cpr}");
                return await _dbContext.Vehicles
                    .Include(vehicle => vehicle.Owner)
                    .Where(vehicle => vehicle.Owner.Cpr.Equals(cpr))
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/VehicleRepo.GetByOwnerAsync| : Error : {e.Message}");
                throw new Exception($"Cannot retrieve the vehicles owned by the customer with cpr '{cpr}'");
            }
        }
        
        public async Task<List<Vehicle>> GetByApprovalStatusAsync(bool isApproved)
        {
            try
            {
                Log.AddLog($"|Repositories/VehicleRepo.GetByApprovalStatusAsync| : Request :  IsApproved:{isApproved}");
                return await _dbContext.Vehicles
                    .Include(vehicle => vehicle.Owner)
                    .Where(vehicle => vehicle.IsApproved == isApproved)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/VehicleRepo.GetByApprovalStatusAsync| : Error : {e.Message}");
                throw new Exception($"Cannot retrieve the vehicles with approved status of '{isApproved}'");
            }
        }

        public async Task<Vehicle> UpdateAsync(Vehicle vehicle)
        {
            try
            {
                _dbContext.Update(vehicle);
                _dbContext.Entry(vehicle.Owner).State = EntityState.Unchanged;
                await _dbContext.SaveChangesAsync();
                Log.AddLog($"|Repositories/VehicleRepo.UpdateAsync| : Reply : {JsonSerializer.Serialize(vehicle)}");
                return vehicle;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/VehicleRepo.UpdateAsync| : Error : {e.Message}");
                throw new Exception($"Did not find vehicle with licenseNo of {vehicle.LicenseNo}");
            }
        }
        

        public async Task<bool> RemoveAsync(string licenseNo)
        {
            var toRemove = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.LicenseNo.Equals(licenseNo));
            if (toRemove == null) return false;
            try
            {
                Log.AddLog($"|Repositories/VehicleRepo.RemoveAsync| : Request : LicenseNo:{licenseNo}");
                _dbContext.Vehicles.Remove(toRemove);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/VehicleRepo.RemoveAsync| : Error : {e.Message}");
                throw new Exception($"Cannot remove the vehicle with licenseNo #{licenseNo}");
            }

        }
    }
}