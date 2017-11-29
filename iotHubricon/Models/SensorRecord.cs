using System;
using System.ComponentModel.DataAnnotations;

namespace iotHubricon.Models
{
    public class SensorRecord
    {
        public Guid SensorRecordId { get; set; }
        public Guid SensorId { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public float Temperature { get; set; }
        [DisplayFormat(DataFormatString = "{0:0.##}")]
        public float Humidity { get; set; }
    }
}