using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDI.Providers.SprocTesting
{
    public class TestParamSetting
    {
        public string ParameterName { get; set; }
        public string TestWith { get; set; }
        public Type TestAs { get; set; }
    }
}
