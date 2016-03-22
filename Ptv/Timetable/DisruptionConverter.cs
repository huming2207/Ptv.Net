using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ptv.Properties;

namespace Ptv.Timetable
{
    public class DisruptionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Disruption))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var disruptionMessage = JObject.Load(reader);

            if (disruptionMessage.Property("metro-train") != null && disruptionMessage.Property("metro-bus") != null && disruptionMessage.Property("regional-bus") != null && disruptionMessage.Property("regional-train") != null && disruptionMessage.Property("regional-bus") != null && disruptionMessage.Property("regional-coach") != null && disruptionMessage.Property("metro-tram") != null && disruptionMessage.Property("general") != null)
            {
                var disruptionCollection = disruptionMessage.ToObject<Disruption>();
                return disruptionCollection;
            }
            else
            {
                throw new TimetableException(Resources.UnexpectedResponseFromServerDetectedTimetableExceptionMessage)
                {
                    Json = disruptionMessage.ToString()
                };
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
