using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace Entity.ModelData
{
    [GraphQLDescription("The account of a customer.")]
    public class Account
    {
        [Key]
        [GraphQLDescription("Customer's username.")]
        public string Username { get; set; }
        
        [Required]
        [GraphQLDescription("Customer's password.")]
        public string Password { get; set; }
        
        [GraphQLDescription("Owner of the account.")]
        public Customer Customer { get; set; }
    }
}