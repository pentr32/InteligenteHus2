using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class THMeasurementsController : ControllerBase
    {
        private readonly IDataStore<THMeasurement> _dataService;

        public THMeasurementsController(IDataStore<THMeasurement> dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<THMeasurement>>> Get()
        {
            return Ok(await _dataService.GetCurrentMeasurementAsync(false));
        }
    }
}
