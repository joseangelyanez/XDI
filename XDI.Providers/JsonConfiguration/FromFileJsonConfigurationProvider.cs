using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Core.DependecyInjection;

namespace XDI.Providers.JsonConfiguration
{
    public class FromFileJsonConfigurationProvider : IJsonConfigurationProvider
    {
        private JObject _main = null;
        private string _filename = null;

        public FromFileJsonConfigurationProvider(string filename)
        {
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));

            _filename = filename;
        }

        public JObject GetJson()
        {
            if (_main == null)
                _main = JObject.Parse(File.ReadAllText(_filename));

            return _main;
        }
    }
}
