using System.Threading.Tasks;
using Database_EFC.Repositories;
using Entity.ModelData;
using HotChocolate;

namespace CarSharing_Database_GraphQL.Queries
{
    // Queries for Coupons
    public partial class Query
    {
        [GraphQLDescription("Get a coupon by code.")]
        public async Task<Coupon> GetCoupon([Service] ICouponRepo couponService, string code)
        {
            return await couponService.GetAsync(code);
        }
    } 
}