using System.ComponentModel.DataAnnotations;
using Entity.ModelData.Behaviors;
using HotChocolate;

namespace Entity.ModelData
{
    [GraphQLDescription("The vehicle owned by a customer.")]
    public class Vehicle : ISoftDeletable
    {
        [Key]
        [GraphQLDescription("Vehicle's license number.")]
        public string LicenseNo { get; set; }
        
        [GraphQLDescription("Vehicle's brand.")]
        public string Brand { get; set; }
        
        [GraphQLDescription("Vehicle's model.")]
        public string Model { get; set; }
        
        [GraphQLDescription("Vehicle's type {Van, SUV, Sedan, Coupe, Hatchback, Pickup truck}.")]
        public string Type { get; set; }
        
        [GraphQLDescription("Vehicle's transmission {Manual, Automatic}.")]
        public string Transmission { get; set; }
        
        [GraphQLDescription("Vehicle's fuel type {Electric, Diesel, Petrol, Hybrid, Hydrogen}.")]
        public string FuelType { get; set; }
        
        [GraphQLDescription("Vehicle's number of seats.")]
        public int Seats { get; set; }
        
        [GraphQLDescription("Vehicle's manufacture year.")]
        public int ManufactureYear { get; set; }
        
        [GraphQLDescription("Vehicle's mileage.")]
        public double Mileage { get; set; }
        
        [GraphQLDescription("Vehicle's owner.")]
        public Customer Owner { get; set; }
        
        [GraphQLIgnore]
        public bool IsDeleted { get; set; }
        
        // is approved by the admin
        [GraphQLDescription("Vehicle's approval status. If it was approved by the admin or not.")]
        public bool IsApproved { get; set; }
    }
    
    public static class VehicleTransmission
    {
        public const string Manual = "Manual";
        public const string Automatic = "Automatic";
    }

    public static class VehicleType
    {
        public const string Van = "Van";
        public const string Suv = "SUV";
        public const string Sedan = "Sedan";
        public const string Coupe = "Coupe";
        public const string Hatchback = "Hatchback";
        public const string PickupTruck = "Pickup Truck";
    }

    public static class VehicleFuelType
    {
        public const string Electric = "Electric";
        public const string Diesel = "Diesel";
        public const string Petrol = "Petrol";
        public const string Hybrid = "Hybrid";
        public const string Hydrogen = "Hydrogen";
    }

}