using System;

namespace CarSharing_Database_GraphQL.Mutations.Records.LeaseRecords
{
    public record UpdateLeaseInput(
        int Id,
        DateTime LeasedFrom,
        DateTime LeasedTo,
        bool IsCanceled,
        decimal TotalPrice,
        ListingKey Listing,
        CustomerKey Customer
    );
}