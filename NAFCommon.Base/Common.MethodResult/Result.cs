using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NAFCommon.Base
{
    public class Result
    {
        public Result()
        {
            Values = new List<string>();
        }

        [JsonPropertyName("Code")]
        public string Code { get; set; }

        [JsonPropertyName("Message")]
        public string Message { get; set; }

        [JsonPropertyName("Values")]
        public List<string> Values { get; set; }
    }
}
