using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NAFCommon.Base.Common.MethodResult
{
    public class ErrorHelpers
    {

        private static ConcurrentDictionary<string, Dictionary<string, string>> _errorMessages;
        public static string GenerateErrorResult(string propertyName, object propertyValue)
        {
            return $"{propertyName.Substring(0, 1).ToLower(CultureInfo.InvariantCulture)}{propertyName.Substring(1, propertyName.Length - 1)}: {propertyValue}";
        }
        public static string GetErrorMessage(string errorCode, Assembly resourceAssembly)
        {
            return GetErrorMessage(errorCode, ref _errorMessages, resourceAssembly);
        }
        public static string GetErrorMessage(string errorCode, ref ConcurrentDictionary<string, Dictionary<string, string>> errorMessages, Assembly resourceAssembly)
        {
            string defaultErrorMessage = "No pre-defined error message";

            if (resourceAssembly == null) return defaultErrorMessage;

            Dictionary<string, string> messages = null;

            var currentLanguage = "vn";

            var dictionaryKey = $"{resourceAssembly.GetName().Name}";

            #region get list of errors

            try
            {
                if (errorMessages != null)
                {
                    messages = errorMessages[dictionaryKey];
                }
            }
            catch { }

            if (messages == null)
            {
                try
                {
                    string jsonErrorFilePath = $"{Settings.ResourceFolderName}.{Settings.ErrorsFileName}-{currentLanguage}.json";
                    //string jsonErrorFilePath = $"{Settings.ResourceFolderName}.{Settings.ErrorsFileName}.json";
                    var fileData = GetFromResources(jsonErrorFilePath, resourceAssembly);

                    messages = JsonSerializer.Deserialize<Dictionary<string, string>>(fileData);
                }
                catch
                {
                    messages = new Dictionary<string, string>();
                }

                if (messages != null)
                {
                    errorMessages ??= new ConcurrentDictionary<string, Dictionary<string, string>>();

                    errorMessages[dictionaryKey] = messages;
                }
            }

            #endregion get list of errors

            defaultErrorMessage = messages.Keys.Contains(errorCode) ? messages[errorCode] : defaultErrorMessage;

            return defaultErrorMessage;
        }
        public static string GetFromResources(string resourceName, Assembly resourceAssembly)
        {
            using Stream stream = resourceAssembly.GetManifestResourceStream(resourceAssembly.GetName().Name + '.' + resourceName);

            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}
