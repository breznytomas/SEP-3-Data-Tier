using System;
using System.Collections.Generic;
using System.Linq;
using CarSharing_Database.ModelData;
using CarSharing_Database.Persistence;

namespace CarSharing_Database.Dao
{
    public class VehicleDao: IVehicleDao
    {
        private static VehicleDao _instance;

        public static VehicleDao Instance => _instance ??= new VehicleDao();

        private readonly IList<Vehicle> _vehicles;

        private VehicleDao()
        {
            var fileContext = FileContext.Instance;
            _vehicles = fileContext.Vehicles;
        }
        
        public Vehicle Create(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public Vehicle Read(string licenseNo)
        {
            return _vehicles.First(vehicle => vehicle.LicenseNo.Equals(licenseNo));
        }
        
        public bool Update(Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string licenseNo)
        {
            throw new NotImplementedException();
        }
    }
}