using CarSharing_Database.ModelData;

namespace CarSharing_Database.Dao
{
    public interface IVehicleDao
    {
        Vehicle Create(Vehicle vehicle);
        Vehicle Read(string licenseNo);
        bool Update(Vehicle vehicle);
        bool Delete(string licenseNo);
    }
}