using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptv.Timetable
{
    public abstract class Location : Item
    {
        [JsonProperty(PropertyName = "suburb")]
        public string Suburb { get; set; }

        [JsonProperty(PropertyName = "location_name")]
        public string LocationName { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "lon")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "distance")]
        public double Distance { get; set; }

        [JsonProperty(PropertyName = "gps", NullValueHandling = NullValueHandling.Include)]
        private Gps _GpsInfo;
        public Gps GPSInfo
        {
            get
            {
                if(_GpsInfo == null)
                {
                    Gps gps = new Gps();
                    gps.Latitude = 0d;
                    gps.Longitude = 0d;
                    return gps;
                }
                else
                {
                    return _GpsInfo;
                }
            }
            set
            {
                _GpsInfo = value;
            }
        }
    }
}
