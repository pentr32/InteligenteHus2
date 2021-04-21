using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class DataStore : IDataStore<THMeasurement>
    {
        readonly List<THMeasurement> thMeasurements;

        public DataStore()
        {
            thMeasurements = new List<THMeasurement>()
            {
                new THMeasurement { Humidity = 45.2f, Temperature = 22.02f, UpdatedMeasurementTime = new DateTime(2021, 04, 10, 15, 13, 50) },
                new THMeasurement { Humidity = 43.5f, Temperature = 21.22f, UpdatedMeasurementTime = new DateTime(2021, 04, 11, 07, 30, 01) },
                new THMeasurement { Humidity = 47.1f, Temperature = 20.72f, UpdatedMeasurementTime = new DateTime(2021, 04, 12, 12, 25, 05) },
                new THMeasurement { Humidity = 49.9f, Temperature = 22.72f, UpdatedMeasurementTime = new DateTime(2021, 04, 13, 13, 55, 42) },
                new THMeasurement { Humidity = 42.9f, Temperature = 30.01f, UpdatedMeasurementTime = new DateTime(2021, 04, 14, 13, 55, 42) },
                new THMeasurement { Humidity = 47.9f, Temperature = 26.37f, UpdatedMeasurementTime = new DateTime(2021, 04, 15, 13, 55, 42) },
                new THMeasurement { Humidity = 46.9f, Temperature = 25.39f, UpdatedMeasurementTime = new DateTime(2021, 04, 16, 13, 55, 42) },
                new THMeasurement { Humidity = 43.9f, Temperature = 21.57f, UpdatedMeasurementTime = new DateTime(2021, 04, 17, 13, 55, 42) },
                new THMeasurement { Humidity = 41.9f, Temperature = 29.08f, UpdatedMeasurementTime = new DateTime(2021, 04, 18, 13, 55, 42) }
            };
        }

        public async Task<THMeasurement> GetCurrentMeasurementAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(thMeasurements.OrderByDescending(x => x.UpdatedMeasurementTime).FirstOrDefault());
        }

        //public async Task<THMeasurement> GetCurrentMeasurementAsync()
        //{
        //    THMeasurement currentMeasurement = thMeasurements.OrderBy(x => x.UpdatedMeasurementTime)
        //}
    }
}
