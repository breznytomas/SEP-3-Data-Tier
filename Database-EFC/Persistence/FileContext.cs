using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Entity.ModelData;

namespace Database_EFC.Persistence
{
    public class FileContext
    {
        private static FileContext _instance;

        public static FileContext Instance =>_instance ??= new FileContext();

        public IList<Vehicle> Vehicles { get; private set; }
        public IList<Listing> Listings { get; private set; }

        private const string VehicleFile = "vehicle.json";
        private const string ListingFile = "vehicleListing.json";
        
        
        private FileContext()
        {
            Vehicles = File.Exists(VehicleFile) ? ReadData<Vehicle>(VehicleFile) : new List<Vehicle>();
            Listings = File.Exists(ListingFile) ? ReadData<Listing>(ListingFile) : new List<Listing>();

            //TODO 24.10 by Ion delete this block of code, used for testing
            if (!File.Exists(ListingFile) || !File.Exists(VehicleFile))
            {
                Seed();
                SaveChanges(Vehicles, VehicleFile);
                SaveChanges(Listings, ListingFile);
            }
        }
        
        private void Seed()
        {
            Vehicle[] v =
            {
                new()
                {
                    Brand = "Tesla",
                    Model = "X",
                    Type = VehicleType.Suv,
                    FuelType = VehicleFuelType.Electric,
                    Transmission = VehicleTransmission.Automatic,
                    Seats = 7,
                    LicenseNo = "XZ01334",
                    ManufactureYear = 2018,
                    Mileage = 50_266,
                },
                new()
                {
                    Brand = "BMW",
                    Model = "m3",
                    Type = VehicleType.Sedan,
                    FuelType = VehicleFuelType.Petrol,
                    Transmission = VehicleTransmission.Manual,
                    Seats = 5,
                    LicenseNo = "AB11222",
                    ManufactureYear = 2014,
                    Mileage = 170_335,
                },
                new()
                {
                    Brand = "Mercedes-Benz",
                    Model = "CLS",
                    Type = VehicleType.Coupe,
                    FuelType = VehicleFuelType.Diesel,
                    Transmission = VehicleTransmission.Automatic,
                    Seats = 5,
                    LicenseNo = "MK99222",
                    ManufactureYear = 2020,
                    Mileage = 10_866,
                }
            };
            Vehicles = v.ToList();

            Listing[] l =
            {
                new()
                {
                    Vehicle = Vehicles[0],
                    ListedDate = new DateTime(2021, 10, 10, 8, 45, 00, DateTimeKind.Utc),
                    Location = "Aarhus",
                    Price = 350.5m,
                    DateFrom = new DateTime(2021, 10, 15, 15, 30, 00, DateTimeKind.Utc), //15 Oct 2021
                    DateTo = new DateTime(2021, 12, 15, 20, 00, 00, DateTimeKind.Utc) //15 Dec 2021
                },
                new()
                {
                    Vehicle = Vehicles[1],
                    ListedDate = new DateTime(2021, 10, 24, 21, 15, 00, DateTimeKind.Utc),
                    Location = "Horsens",
                    Price = 200.8m,
                    DateFrom = new DateTime(2021, 10, 24, 15, 30, 00, DateTimeKind.Utc), //24 Oct 2021
                    DateTo = new DateTime(2021, 11, 24, 15, 30, 00, DateTimeKind.Utc) //24 Nov 2021
                },
                new()
                {
                    Vehicle = Vehicles[2],
                    ListedDate = new DateTime(2021, 10, 15, 21, 15, 00, DateTimeKind.Utc),
                    Location = "Aarhus",
                    Price = 500m,
                    DateFrom = new DateTime(2021, 10, 15, 22, 00, 00, DateTimeKind.Utc), //15 Oct 2021
                    DateTo = new DateTime(2022, 1, 30, 12, 00, 00, DateTimeKind.Utc) //1 Jan 2022
                }
            };
            Listings = l.ToList();
        }

        private IList<T> ReadData<T>(string s)
        {
            using var jsonReader = File.OpenText(s);
            return JsonSerializer.Deserialize<List<T>>(jsonReader.ReadToEnd(), new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public void SaveChanges<T>(T toSerialize, string path)
        {
            var json = JsonSerializer.Serialize(toSerialize, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            using var outputFile = new StreamWriter(path, false);
            outputFile.Write(json);
            
        }
    }
}