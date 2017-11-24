using System;

namespace iotHubricon.Models
{
    public class SensorRecord
    {
        public Guid SensorRecordId { get; set; }
        public Guid SensorId { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
    }
}