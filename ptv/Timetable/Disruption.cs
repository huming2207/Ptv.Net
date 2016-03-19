using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ptv.Timetable
{
    [JsonObject()]
    public class Disruption : Item
    {
        // TO-DO: THESE STUFF BELOW DOESN'T WORK AT ALL
        // COMPLETELY WRONG, SILLY ME...
        // NEED TO BE FIXED BY WRITING ANOTHER "ItemConverter"
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string MessageURL { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "publishedOn")]
        public DateTime PublishTime { get; set; }
    }
}
