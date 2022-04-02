using System.Threading.Tasks;
using Entity.ModelData;

namespace Database_EFC.Repositories
{
    public interface ICouponRepo
    {
        Task<Coupon> AddAsync(Coupon coupon);
        Task<Coupon> GetAsync(string code);
    }
}