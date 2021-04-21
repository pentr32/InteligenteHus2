using System;

namespace WebAPI.Models
{
    public class THMeasurement
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public DateTime UpdatedMeasurementTime { get; set; }
    }
}
