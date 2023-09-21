using System.Text.Json.Serialization;

namespace NAFCommon.Base.Common.MethodResult
{
    public class MethodResult<T> : VoidMethodResult
    {
        [JsonPropertyName("result")]
        public T Result { get; set; }
    }
}