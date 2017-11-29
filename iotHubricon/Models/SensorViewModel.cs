using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iotHubricon.Models
{
    public class SensorViewModel
    {
        public Dictionary<string, float> HumidityAverage { get; set; } = new Dictionary<string, float>();
        public Dictionary<string, float> TemperatureAverage { get; set; } = new Dictionary<string, float>();
        public List<Sensor> Sensors { get; set; }
        public Dictionary<string, List<DateValueRecord>> LastTemperatureRecords { get; set; } = new Dictionary<string, List<DateValueRecord>>();
        public Dictionary<string, List<DateValueRecord>> LastHumidityRecords { get; set; } = new Dictionary<string, List<DateValueRecord>>();
    }

    public class DateValueRecord
    {
        public DateValueRecord(DateTime date, float value)
        {
            Date = date;
            Value = value;
        }

        public DateTime Date { get; set; }
        public float Value { get; set; }
    }
}