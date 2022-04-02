using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace Entity.ModelData
{
    [GraphQLDescription("The coupon discount.")]
    public class Coupon
    {
        [Key]
        [GraphQLDescription("Coupon's code.")]
        public string Code { get; set; }
        
        [GraphQLDescription("Coupon's discount. Decimal number between 0 and 1.")]
        public double Discount { get; set; }
    }
}