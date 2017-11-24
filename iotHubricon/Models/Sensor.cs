using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iotHubricon.Models
{
    public class Sensor
    {
        public Guid SensorId { get; set; }
        public string Name { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Location { get; set; }

        //public Point LocationPoint { get; set; }
    }
}