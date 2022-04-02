using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database_EFC.Repositories;
using Entity.ModelData;
using HotChocolate;

namespace CarSharing_Database_GraphQL.Queries
{
    // Queries for Listings
    public partial class Query
    {
        [GraphQLDescription("Get a list of listings by location, and the date-time 'from' and 'to'.")]
        public async Task<IList<Listing>> GetListing([Service] IListingRepo listingRepo, string location, DateTime dateFrom, DateTime dateTo)
        {
            return await listingRepo.GetAsync(location, dateFrom, dateTo);
        }
        
        [GraphQLDescription("Get a list of listings by a specific vehicle's license number.")]
        public async Task<IList<Listing>> GetListingsByVehicle([Service] IListingRepo listingRepo, string licenseNo)
        {
            return await listingRepo.GetByVehicleAsync(licenseNo);
        }
        
        [GraphQLDescription("Get a listing by id.")]
        public async Task<Listing> GetListingById([Service] IListingRepo listingRepo, int id)
        {
            return await listingRepo.GetByIdAsync(id);
        }
    }
}