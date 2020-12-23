using log4net;
using Newtonsoft.Json;
using System;

namespace PracticeApi.Extensions.Newtonsoft
{
    /// <summary>
    /// long to string
    /// </summary>
    public class LongStringConverter : JsonConverter<long>
    {
        private readonly ILog _log = LogManager.GetLogger(typeof(LongStringConverter));

        ///<inheritdoc/>
        public override long ReadJson(JsonReader reader, Type objectType, long existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            try
            {
                var v = Convert.ToInt64(reader.Value);
                return v;
            }
            catch (Exception e)
            {
                _log.Error($"long to string error:{reader.ReadAsString()}", e);
                throw;
            }
        }

        ///<inheritdoc/>
        public override void WriteJson(JsonWriter writer, long value, JsonSerializer serializer) => writer.WriteValue(value.ToString());
    }
}
