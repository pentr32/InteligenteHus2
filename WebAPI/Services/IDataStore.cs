using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IDataStore<T>
    {
        Task<T> GetCurrentMeasurementAsync(bool forceRefresh = false);
    }
}
