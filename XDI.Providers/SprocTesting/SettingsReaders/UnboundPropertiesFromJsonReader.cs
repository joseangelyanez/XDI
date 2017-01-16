using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Providers.SprocTesting;

namespace XDI.Providers.SprocTesting.SettingsReaders
{
    public class UnboundPropertiesFromJsonReader
    {
        public IEnumerable<UnboundPropertySetting> ReadUnboundPropertiesFromJson(JObject jobject)
        {
            if (jobject == null)
                throw new ArgumentNullException(nameof(jobject));

            if (jobject["unboundProperties"] == null)
                return null;

            List<UnboundPropertySetting> unboundPropertySettings = new List<UnboundPropertySetting>();

            foreach (var unboundProperty in jobject["unboundProperties"])
            {
                UnboundPropertySetting unboundPropertySetting = new UnboundPropertySetting();

                var property = ((Newtonsoft.Json.Linq.JProperty)unboundProperty.First);
                if (property == null || property.Name == null || property.Value == null)
                    throw new XDIJsonSettingsException("There was a problem parsing the entry in 'unboundProperties'.");

                string propertyName = property.Name;
                string propertyValue = property.Value.ToString();

                unboundPropertySettings.Add(unboundPropertySetting);
            }

            return unboundPropertySettings;
        }
    }
}
