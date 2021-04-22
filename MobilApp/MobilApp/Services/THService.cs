using MobilApp.Constants;
using MobilApp.Models;
using MobilApp.Repository;
using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TinyIoC;
using Xamarin.Essentials;

namespace MobilApp.Services
{
    public class THService : ITHService
    {
        private readonly IGenericRepository _genericRepository;
        THMeasurement myMeasurement;
        
        public THService()
        {
            _genericRepository = TinyIoCContainer.Current.Resolve<IGenericRepository>();
            myMeasurement = new THMeasurement();
        }

        public async Task<THMeasurement> GetCurrentMeasurementAsync()
        {

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.THMeasurementsEndpoint,
                Query = "results=1"
            };

            string url = builder.Path;

            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                return Barrel.Current.Get<THMeasurement>(key: url);
            }
            if (!Barrel.Current.IsExpired(key: url))
            {
                return Barrel.Current.Get<THMeasurement>(key: url);
            }
            var measurement = await _genericRepository.GetAsync<Root>(builder.ToString());

            Barrel.Current.Add(key: url, data: measurement, expireIn: TimeSpan.FromSeconds(20));

            if(measurement != null)
            {
                myMeasurement.Temperature = float.Parse(measurement.feeds[0].field1);
                myMeasurement.Humidity = float.Parse(measurement.feeds[0].field2);
                myMeasurement.UpdatedMeasurementTime = measurement.feeds[0].created_at;
            }

            return myMeasurement;
        }

        public async Task<IEnumerable<THMeasurement>> GetAllMeasurementsAsync(MeasurementFilterBy FilterBy)
        {
            int queryResults = 0;
            switch (FilterBy)
            {
                case MeasurementFilterBy.ByNewestTime:
                    queryResults = 3;
                    break;

                case MeasurementFilterBy.ByNewestDay:
                    queryResults = 6;
                    break;

                case MeasurementFilterBy.ByNewestWeek:
                    queryResults = 15;
                    break;

                default:
                    break;
            }

            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = $"{ApiConstants.THMeasurementsEndpoint}",
                Query = $"results={queryResults.ToString()}"
            };

            string url = builder.Path + builder.Query + "List";

            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                return Barrel.Current.Get<IEnumerable<THMeasurement>>(key: url);
            }
            if (!Barrel.Current.IsExpired(key: url))
            {
                return Barrel.Current.Get<IEnumerable<THMeasurement>>(key: url);
            }
            var measurements = await _genericRepository.GetAsync<Root>(builder.ToString());

            Barrel.Current.Add(key: url, data: measurements, expireIn: TimeSpan.FromSeconds(20));

            List<THMeasurement> thMeasurements = new List<THMeasurement>();

            if(measurements != null)
            {
                foreach(var feed in measurements.feeds)
                {
                    thMeasurements.Add(new THMeasurement
                    {
                        Temperature = float.Parse(feed.field1),
                        Humidity = float.Parse(feed.field2),
                        UpdatedMeasurementTime = feed.created_at
                    });
                }
            }

            return thMeasurements;
        }
    }
}