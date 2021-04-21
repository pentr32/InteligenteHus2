using MobilApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobilApp.Services
{
    public interface ITHService
    {
        Task<THMeasurement> GetCurrentMeasurementAsync();
    }
}
