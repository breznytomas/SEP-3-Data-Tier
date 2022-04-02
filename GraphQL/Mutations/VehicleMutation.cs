using System.Threading.Tasks;
using CarSharing_Database_GraphQL.Mutations.Records.VehicleRecords;
using Database_EFC.Repositories;
using Entity.ModelData;
using HotChocolate;

namespace CarSharing_Database_GraphQL.Mutations
{
    public partial class Mutation
    {
        [GraphQLDescription("Add a vehicle and associate it with a customer.")]
        public async Task<Vehicle> AddVehicle([Service] IVehicleRepo vehicleRepo, AddVehicleInput input)
        {
            var vehicle = new Vehicle
            {
                 LicenseNo = input.LicenseNo,
                 Brand = input.Brand,
                 Model = input.Model,
                 Type = input.Type,
                 Transmission = input.Transmission,
                 FuelType = input.FuelType,
                 Seats = input.Seats,
                 ManufactureYear = input.ManufactureYear,
                 Mileage = input.Mileage,
                 Owner = new Customer
                 {
                     Cpr = input.Owner.Cpr
                 }
            };
            return await vehicleRepo.AddAsync(vehicle);
        }
        
        [GraphQLDescription("Update a vehicle by its license number.")]
        public async Task<Vehicle> UpdateVehicle([Service] IVehicleRepo vehicleRepo, UpdateVehicleInput input)
        {
            var vehicle = new Vehicle
            {
                LicenseNo = input.LicenseNo,
                Brand = input.Brand,
                Model = input.Model,
                Type = input.Type,
                Transmission = input.Transmission,
                FuelType = input.FuelType,
                Seats = input.Seats,
                ManufactureYear = input.ManufactureYear,
                Mileage = input.Mileage,
                IsApproved = input.IsApproved,
                Owner = new Customer
                {
                    Cpr = input.Owner.Cpr
                }
            };
            return await vehicleRepo.UpdateAsync(vehicle);
        }
        
        [GraphQLDescription("Remove a vehicle by its license number.")]
        public async Task<bool> RemoveVehicle([Service] IVehicleRepo vehicleRepo, string licenseNo)
        {
            return await vehicleRepo.RemoveAsync(licenseNo);
        }
    }
}