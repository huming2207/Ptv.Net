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
        [JsonProperty(PropertyName = "disruption_id")]
        public string DisruptionId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string MessageURL { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName ="status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "publishedOn")]
        public DateTime PublishTime { get; set; }
    }

    // Something strange here that PTV themselves may forget to mention two Json Properties in their offical documentation.
    // Don't know why. These two properties do exist in actual replies, called "fromDate" and "toDate", which are two DateTime-type data,
    // indicate the time of their announcements start and end. But because that they were not offically mentioned by 
    // the PTV, so I won't add them into this library.
    //
    // Here are one example:
    // ... ... ...
    //  {
    //      "disruption_id": 51443,
    //      "title": "Temporary car park closure at Blackburn Station: Monday 7 March - Monday, 18 April 2016\t",
    //      "url": "http://ptv.vic.gov.au/live-travel-updates/article/tue-23-feb-metropolitan-trains-belgrave-linelilydale-line",
    //      "description": "Due to works as part of the Blackburn Level Crossing Removal Project, sections of the Railway Road car park will be temporarily closed from 6am on Monday 7 March, until 6am on Monday, 18 April 2016.",
    //      "status": "Current",
    //      "publishedOn": "2016-02-23T19:35:21Z",
    //      "fromDate": "2016-03-06T16:00:00Z",
    //      "toDate": "2016-04-17T20:00:00Z"  --> Sometimes "toDate" value will be set to null, if PTV doesn't figure out when they will finish their works.
    //  },
    // ... ... ...
    
}
