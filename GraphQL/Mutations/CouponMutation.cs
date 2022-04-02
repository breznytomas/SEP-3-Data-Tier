using System.Threading.Tasks;
using Database_EFC.Repositories;
using Entity.ModelData;
using HotChocolate;

namespace CarSharing_Database_GraphQL.Mutations
{
    public partial class Mutation
    {
        [GraphQLDescription("Add a coupon discount.")]
        public async Task<Coupon> AddCoupon([Service] ICouponRepo couponRepo, Coupon coupon)
        {
            return await couponRepo.AddAsync(coupon);
        }
    }
}