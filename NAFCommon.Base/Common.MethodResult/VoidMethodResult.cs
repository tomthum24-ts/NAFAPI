using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NAFCommon.Base.Common.MethodResult
{
    public class VoidMethodResult
    {
    

        private readonly List<Result> _Messages = new List<Result>();

        #region error message

        public void AddMessage(Result result) => _Messages.Add(result);

        public void AddMessages(IEnumerable<Result> results) => _Messages.AddRange(results);

        public void AddMessage(string exceptionErrorMessage, string exceptionStackTrace = "")
        {
            AddMessage(CommonErrors.APIServerError, "Error", new string[] { }, exceptionErrorMessage, exceptionStackTrace);
        }

        public void AddErrorMessage(string code, string message, string[] values)
        {
            var result = new Result
            {
                Code = code,
                Message = message
            };

            if (values?.Length > 0)
            {
                foreach (var value in values)
                    result.Values.Add(value);
            }

            AddMessage(result);
        }

        private void AddMessage(
            string code,
            string message,
            string[] values,
            string exceptionMessage,
            string exceptionStackTrace)
        {
            _Messages.Add(new Result
            {
                Code = code,
                Message = $"Error: {message}, Exception Message: {exceptionMessage}, Stack Trace: {exceptionStackTrace}",
                Values = new List<string>(values)
            });
        }

        #endregion error message


        [JsonPropertyName("Messages")]
        public IReadOnlyCollection<Result> Messages => _Messages;

        [JsonPropertyName("isOk")] public bool IsOk => _Messages.Count == 0;

        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
