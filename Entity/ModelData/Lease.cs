using System;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace Entity.ModelData
{
    [GraphQLDescription("The lease for a listing.")]
    public class Lease
    {
        [Key]
        [GraphQLDescription("Lease's id.")]
        public int Id { get; set; }
        
        [Required]
        [GraphQLDescription("Date from when the vehicle is rented.")]
        public DateTime LeasedFrom { get; set; }
        
        [Required]
        [GraphQLDescription("Date until the vehicle is rented.")]
        public DateTime LeasedTo { get; set; }
        
        [GraphQLDescription("Lease's status. If it was canceled by owner or customer.")]
        public bool Canceled { get; set; }
        
        [GraphQLDescription("Lease's total price.")]
        public decimal TotalPrice { get; set; }
        
        [GraphQLDescription("Lease's listing associated to.")]
        public Listing Listing { get; set; }
        
        [GraphQLDescription("Lease's customer.")]
        public Customer Customer { get; set; }
    }
    
}