namespace CarSharing_Database_GraphQL.Mutations.Records.VehicleRecords
{ 
    public record AddVehicleInput(
        string LicenseNo,
        string Brand,
        string Model,
        string Type,
        string Transmission,
        string FuelType,
        int Seats,
        int ManufactureYear,
        double Mileage,
        CustomerKey Owner
    );
}