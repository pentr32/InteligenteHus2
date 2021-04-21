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
        public THService()
        {
            _genericRepository = TinyIoCContainer.Current.Resolve<IGenericRepository>();
        }

        public async Task<THMeasurement> GetCurrentMeasurementAsync()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.THMeasurementsEndpoint
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
            Thread.Sleep(3000); // Simulerer 3 sekunders forsinkelte
            var measurement = await _genericRepository.GetAsync<THMeasurement>(builder.ToString());
            //Saves the cache and pass it a timespan for expiration
            Barrel.Current.Add(key: url, data: measurement, expireIn: TimeSpan.FromSeconds(20));
            return measurement;
        }
    }
}
