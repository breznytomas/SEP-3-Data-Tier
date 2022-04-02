using System;
using System.Text.Json;
using System.Threading.Tasks;
using Database_EFC.Persistence;
using Entity.ModelData;
using Logger.Log;
using Microsoft.EntityFrameworkCore;

namespace Database_EFC.Repositories.Impl
{
    public class CouponRepo : ICouponRepo
    {
        private readonly CarSharingDbContext _dbContext;

        public CouponRepo(CarSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Coupon> AddAsync(Coupon coupon)
        {
            Log.AddLog($"|Repositories/CouponRepo.AddAsync| : Request : {JsonSerializer.Serialize(coupon)}");
            var added = await _dbContext.Coupons.AddAsync(coupon);
            await _dbContext.SaveChangesAsync();
            return added.Entity;
        }

        public async Task<Coupon> GetAsync(string code)
        {
            try
            {
                Log.AddLog($"|Repositories/CouponRepo.GetAsync| : Request : {code}");
                return await _dbContext.Coupons
                    .FirstAsync(coupon => coupon.Code.Equals(code));
            }
            catch (Exception e)
            {
                Log.AddLog($"|Repositories/CouponRepo.GetAsync| : Error : {e.Message}");
                throw new Exception($"Did not find the coupon with code '{code}'");
            }
        }
    }
}