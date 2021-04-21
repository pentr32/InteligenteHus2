using MobilApp.Constants;
using MobilApp.Models;
using MobilApp.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinyIoC;

namespace MobilApp.Services
{
    public class THService : ITHService
    {
        private readonly IGenericRepository _genericRepository;
        public THService()
        {
            _genericRepository = TinyIoCContainer.Current.Resolve<IGenericRepository>();
        }

        public async Task<THMeasurement> GetCurrentMeasurement()
        {
            UriBuilder builder = new UriBuilder(ApiConstants.BaseApiUrl)
            {
                Path = ApiConstants.THMeasurementsEndpoint
            };
            return await _genericRepository.GetAsync<THMeasurement>(builder.ToString());
        }
    }
}
