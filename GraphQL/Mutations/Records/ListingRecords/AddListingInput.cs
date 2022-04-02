using System;
using Entity.ModelData;

namespace CarSharing_Database_GraphQL.Mutations.Records.ListingRecords
{
    public record AddListingInput(
        DateTime ListedDate,
        decimal Price,
        string Location,
        DateTime DateFrom,
        DateTime DateTo,
        VehicleKey Vehicle
    );
}