using System;
using System.Collections.Generic;
using System.Linq;
using CarSharing_Database.ModelData;
using CarSharing_Database.Persistence;
using Microsoft.VisualBasic;

namespace CarSharing_Database.Dao
{
    public class ListingDao : IListingDao
    {
        private static ListingDao _instance;

        public static ListingDao Instance => _instance ??= new ListingDao();

        private readonly IList<Listing> _listings;

        private ListingDao()
        {
            var fileContext = FileContext.Instance;
            _listings = fileContext.Listings;
        }

        public Listing Create(Listing listing)
        {
            throw new NotImplementedException();
        }

        //TODO 24.10 by Ion return list of Listings
        public IList<Listing> Read(string location, DateTime dateFrom, DateTime dateTo)
        {
            
            return _listings.Where(l => l.Location.Equals(location) &&
                                              l.DateFrom < dateFrom &&
                                              l.DateTo > dateTo).ToList();
        }

        public Listing Read(string location, DateInterval dateInterval)
        {
            throw new NotImplementedException();
        }

        public bool Update(Listing listing)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}