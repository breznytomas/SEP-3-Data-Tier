using System;

namespace CarSharing_Database_GraphQL.Mutations.Records.LeaseRecords
{
    public record AddLeaseInput(
        DateTime LeasedFrom,
        DateTime LeasedTo,
        decimal TotalPrice,
        ListingKey Listing,
        CustomerKey Customer
    );
}