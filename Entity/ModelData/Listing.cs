using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entity.ModelData.Behaviors;
using HotChocolate;
using HotChocolate.Types;

namespace Entity.ModelData
{
    [GraphQLDescription("The listing for a vehicle.")]
    public class Listing : ISoftDeletable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [GraphQLDescription("Listing's id.")]
        public int Id { get; set; }
        
        [GraphQLDescription("Date and time when the listing was created.")]
        public DateTime ListedDate { get; set; }
        [Range(1, int.MaxValue)]
        
        [GraphQLDescription("Listing's price per day in dkk.")]
        public decimal Price { get; set; }
        [MaxLength(250)]
        
        [GraphQLDescription("Listing's location of the vehicle.")]
        public string Location { get; set; }
        
        [GraphQLDescription("Date from when the listing is available.")]
        public DateTime DateFrom { get; set; }
        
        [GraphQLDescription("Date until the listing is available.")]
        public DateTime DateTo { get; set; }
        
        [GraphQLDescription("Listing's vehicle associated to.")]
        public Vehicle Vehicle { get; set; }

        [GraphQLIgnore]
        public bool IsDeleted { get; set; }
    }
}