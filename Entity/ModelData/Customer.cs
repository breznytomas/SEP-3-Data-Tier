using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace Entity.ModelData
{
    [GraphQLDescription("The customer or the owner of a vehicle.")]
    public class Customer
    {
        [Key]
        [GraphQLDescription("Customer's cpr.")]
        public string Cpr { get; set; }
        
        [Required]
        [StringLength(255, MinimumLength = 3)]
        [GraphQLDescription("Customer's firstname.")]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(255, MinimumLength = 3)]
        [GraphQLDescription("Customer's lastname.")]
        public string LastName { get; set; }
        
        [Required]
        [GraphQLDescription("Customer's phone number.")]
        public string PhoneNo { get; set; }
        
        [GraphQLDescription("Customer's access level.")]
        public int AccessLevel { get; set; }
    }
    
}