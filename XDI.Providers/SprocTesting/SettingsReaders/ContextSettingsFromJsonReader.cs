using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Code;
using XDI.Core.DependecyInjection;
using XDI.Providers.JsonConfiguration;
using XDI.Providers.SprocTesting.Settings;

namespace XDI.Providers.SprocTesting.SettingsReaders
{
    public class ContextSettingsFromJsonReader
    {
        private readonly TestParamsFromJsonReader _testParamsReader;
        private readonly UnboundPropertiesFromJsonReader _unboundPropertiesReader;
        private readonly IJsonConfigurationProvider _configurationProvider;

        public ContextSettingsFromJsonReader(
            IJsonConfigurationProvider configurationProvider, 
            TestParamsFromJsonReader testParamsReader,
            UnboundPropertiesFromJsonReader unboundPropertiesReader)
        {
            if (configurationProvider == null)
                _configurationProvider = configurationProvider;
            if (testParamsReader == null)
                throw new ArgumentNullException(nameof(testParamsReader));
            if (unboundPropertiesReader == null)
                throw new ArgumentNullException(nameof(unboundPropertiesReader));

            _testParamsReader = testParamsReader;
            _unboundPropertiesReader = unboundPropertiesReader;
            _configurationProvider = configurationProvider;
        }

        public IEnumerable<ContextSettings> ReadContextSettings()
        {
            /* Gets the main Json object for the root of the file. */
            JObject main = _configurationProvider.GetJson();
    
            /* The 'main' context. */
            var jsonContexts = main["contexts"];

            /* "contexts" element is required. */
            if (main["contexts"] == null)
                throw new XDIJsonSettingsException("The 'contexts' array element in dataflip.json is required.");

            /* Gets an objectifed version of the settings for the current context. */
            return jsonContexts.Select(n => GetContextSettingFromJson(n));  
        }

        private ContextSettings GetContextSettingFromJson(JToken jsonContext)
        {
            ContextSettings settings = new ContextSettings();
            string connectionString = jsonContext["connectionString"]?.Value<string>();
            string nspace = jsonContext["namespace"]?.Value<string>();
            string name = jsonContext["name"]?.Value<string>();
            string output = jsonContext["output"]?.Value<string>();

            if (connectionString == null)
                throw new XDIJsonSettingsException("The '/contexts[]/connectionString' element in dataflip.json is required.");
            if (nspace == null)
                throw new XDIJsonSettingsException("The '/contexts[]/namespace' element in dataflip.json is required.");
            if (name == null)
                throw new XDIJsonSettingsException("The '/contexts[]/name' element in dataflip.json is required.");
            if (output == null)
                throw new XDIJsonSettingsException("The '/contexts[]/output' element in dataflip.json is required.");

            settings.ConnectionString = connectionString;
            settings.Namespace = nspace;
            settings.ContextName = name;
            settings.ContextFilename = output;
            settings.SprocSettings = GetSprocSettingFromJsonContext(jsonContext);

            return settings;
        }

        private IEnumerable<SprocSettings> GetSprocSettingFromJsonContext(JToken jsonContext)
        {
            List<SprocSettings> result = new List<SprocSettings>();

            foreach (var sproc in jsonContext["sprocs"].AsJEnumerable())
            {
                SprocSettings sprocSetting = new SprocSettings();

                string sprocName = sproc["name"].Value<string>();
                string @return = sproc["return"]?.Value<string>();
                string method = sproc["method"]?.Value<string>();
                string comments = sproc["comments"]?.Value<string>();

                string methodOrSproc = method ?? sprocName;

                sprocSetting.Name = methodOrSproc;
                sprocSetting.Return = @return;
                sprocSetting.Comments = comments;
                sprocSetting.Method = method;
                sprocSetting.TestParams = _testParamsReader.ReadTestParamsFromJson(sproc as JObject);
                sprocSetting.UnboundProperties = _unboundPropertiesReader.ReadUnboundPropertiesFromJson(sproc as JObject);

                result.Add(sprocSetting);
            }

            return result;
        }
    }
}
