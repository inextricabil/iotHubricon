using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iotHubricon.Models
{
    public class SensorRecord
    {
        [Key]
        public Guid SensorId { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
    }
}