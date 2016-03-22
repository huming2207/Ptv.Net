﻿using System;
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
            if (objectType == typeof(Disruption[]))
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
            var disruptionWrapper = JObject.Load(reader);

            if (disruptionWrapper.Property("metro-train") != null)
            {
                var disruptionMessage = disruptionWrapper["metro-train"];
                var result = disruptionMessage.ToObject<Disruption[]>();
                return result;
            }
            else if (disruptionWrapper.Property("metro-bus") != null)
            {
                var disruptionMessage = disruptionWrapper["metro-bus"];
                var result = disruptionMessage.ToObject<Disruption[]>();
                return result;
            }
            else if (disruptionWrapper.Property("metro-tram") != null)
            {
                var disruptionMessage = disruptionWrapper["metro-tram"];
                var result = disruptionMessage.ToObject<Disruption[]>();
                return result;
            }
            else if (disruptionWrapper.Property("regional-train") != null)
            {
                var disruptionMessage = disruptionWrapper["regional-train"];
                var result = disruptionMessage.ToObject<Disruption[]>();
                return result;
            }
            else if (disruptionWrapper.Property("regional-bus") != null)
            {
                var disruptionMessage = disruptionWrapper["regional-bus"];
                var result = disruptionMessage.ToObject<Disruption[]>();
                return result;
            }
            else if (disruptionWrapper.Property("regional-coach") != null)
            {
                var disruptionMessage = disruptionWrapper["regional-coach"];
                var result = disruptionMessage.ToObject<Disruption[]>();
                return result;
            }
            else if (disruptionWrapper.Property("general") != null)
            {
                var disruptionMessage = disruptionWrapper["general"];
                var result = disruptionMessage.ToObject<Disruption[]>();
                return result;
            }
            else
            {
                throw new TimetableException(Resources.UnexpectedResponseFromServerDetectedTimetableExceptionMessage)
                {
                    Json = disruptionWrapper.ToString()
                };
            }

            /*
            if (disruptionMessage.Property("metro-train") != null       || 
                disruptionMessage.Property("metro-bus") != null         || 
                disruptionMessage.Property("regional-bus") != null      || 
                disruptionMessage.Property("regional-train") != null    || 
                disruptionMessage.Property("regional-bus") != null      || 
                disruptionMessage.Property("regional-coach") != null    || 
                disruptionMessage.Property("metro-tram") != null        || 
                disruptionMessage.Property("general") != null)
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
            } */


        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
