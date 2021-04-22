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
                new THMeasurement { Humidity = 45.2f, Temperature = 22.02f, UpdatedMeasurementTime = new DateTime(2021, 04, 11, 15, 13, 50) },
                new THMeasurement { Humidity = 43.5f, Temperature = 21.22f, UpdatedMeasurementTime = new DateTime(2021, 04, 11, 07, 30, 01) },
                new THMeasurement { Humidity = 47.1f, Temperature = 20.72f, UpdatedMeasurementTime = new DateTime(2021, 04, 11, 12, 25, 05) },
                new THMeasurement { Humidity = 42.9f, Temperature = 22.72f, UpdatedMeasurementTime = new DateTime(2021, 04, 11, 14, 55, 42) },
                new THMeasurement { Humidity = 20.9f, Temperature = 30.01f, UpdatedMeasurementTime = new DateTime(2021, 04, 16, 14, 40, 32) },
                new THMeasurement { Humidity = 41.9f, Temperature = 27.20f, UpdatedMeasurementTime = new DateTime(2021, 04, 16, 14, 19, 20) },
                new THMeasurement { Humidity = 30.9f, Temperature = 26.10f, UpdatedMeasurementTime = new DateTime(2021, 04, 16, 20, 52, 21) },
                new THMeasurement { Humidity = 40.9f, Temperature = 30.37f, UpdatedMeasurementTime = new DateTime(2021, 04, 22, 17, 30, 50) },
                new THMeasurement { Humidity = 60.9f, Temperature = 21.05f, UpdatedMeasurementTime = new DateTime(2021, 04, 22, 13, 30, 50) },
                new THMeasurement { Humidity = 25.9f, Temperature = 27.19f, UpdatedMeasurementTime = new DateTime(2021, 04, 22, 12, 30, 50) },
                new THMeasurement { Humidity = 10.9f, Temperature = 31.60f, UpdatedMeasurementTime = new DateTime(2021, 04, 22, 13, 30, 50) }
            };
        }

        public async Task<THMeasurement> GetCurrentMeasurementAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(thMeasurements.OrderByDescending(x => x.UpdatedMeasurementTime).FirstOrDefault());
        }

        public async Task<IEnumerable<THMeasurement>> GetMeasurementsByFilterAsync(MeasurementFilterBy FilterBy, bool forceRefresh = false)
        {
            switch (FilterBy)
            {
                case MeasurementFilterBy.NoFilter:
                    return await Task.FromResult(thMeasurements);

                case MeasurementFilterBy.ByNewestTime:
                    return await Task.FromResult(thMeasurements.Where(x => x.UpdatedMeasurementTime >= DateTime.Now.AddHours(-1) && x.UpdatedMeasurementTime <= DateTime.Now));

                case MeasurementFilterBy.ByNewestDay:
                    return await Task.FromResult(thMeasurements.Where(x => x.UpdatedMeasurementTime >= DateTime.Now.AddDays(-1) && x.UpdatedMeasurementTime <= DateTime.Now));

                case MeasurementFilterBy.ByNewestWeek:
                    return await Task.FromResult(thMeasurements.Where(x => x.UpdatedMeasurementTime >= DateTime.Now.AddDays(-7) && x.UpdatedMeasurementTime <= DateTime.Now));

                default:
                    throw new ArgumentOutOfRangeException(nameof(FilterBy), FilterBy, null);
            }
        }
    }
}
