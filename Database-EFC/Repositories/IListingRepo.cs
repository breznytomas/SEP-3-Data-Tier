using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity.ModelData;
using Microsoft.VisualBasic;

namespace Database_EFC.Repositories
{
    public interface IListingRepo
    {
        Task<Listing> AddAsync(Listing listing);
        Task<IList<Listing>> GetAsync(string location, DateTime dateFrom, DateTime dateTo);
        Task<IList<Listing>> GetByVehicleAsync(string licenseNo);
        Task<Listing> GetByIdAsync(int id);
        Task<Listing> GetAsync(string location, DateInterval dateInterval);
        Task<Listing> UpdateAsync(Listing listing);
        Task<bool> RemoveAsync(int id);
    }
}