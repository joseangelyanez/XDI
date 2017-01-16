using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Code;
using XDI.Providers.SprocTesting;

namespace XDI.Providers.SprocTesting.SettingsReaders
{
    public class TestParamsFromJsonReader
    {
        private readonly TypeResolver _typeResolver = null;

        public TestParamsFromJsonReader(TypeResolver typeResolver)
        {
            if (typeResolver == null)
                throw new ArgumentNullException(nameof(typeResolver));

            _typeResolver = typeResolver;
        }

        public IEnumerable<TestParamSetting> ReadTestParamsFromJson(JObject jobject)
        {
            if (jobject == null)
                throw new ArgumentNullException(nameof(jobject));
            if (jobject["testParams"] == null)
                return null; /* <- The sproc doesn't need any testing. */

            List<TestParamSetting> testParams = new List<TestParamSetting>();
            foreach (var testParam in jobject["testParams"])
            {
                TestParamSetting testParamSetting = new TestParamSetting();

                var property = ((JProperty)testParam.First);
                if (property == null || property.Name == null || property.Value == null)
                    throw new XDIJsonSettingsException("There was a problem parsing the entry in 'testParams'.");

                string paramName = property.Name;
                string paramValue = property.Value.ToString();
                string paramType = "string";

                string[] paramValueParts = paramValue.Split(':');
                if (paramValueParts.Length > 1)
                {
                    paramValue = paramValueParts[0];
                    paramType = paramValueParts[1];
                }
                    

                testParamSetting.ParameterName = paramName;
                testParamSetting.TestWith = paramValue;
                testParamSetting.TestAs = _typeResolver.GetClrTypeFromFriendlyName(paramType);

                testParams.Add(testParamSetting);
            }

            return testParams;
        }
    }
}
