using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDI.Providers.SprocTesting
{
    public class SprocSettings
    {
        public IEnumerable<TestParamSetting> TestParams { get; set; }
        public IEnumerable<UnboundPropertySetting> UnboundProperties { get; set; }

        /// <summary>
        /// The name of the sproc as it sits in SQL Server and set in the "name" section of the dataflip.json file.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The name of the automatically generated method and set in the "method" section of the dataflip.json file.
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// The return type for the sproc and set in the "return" section of the dataflip.json file.
        /// </summary>
        public string Return { get; set; }
        /// <summary>
        /// The comments to use on top of the sproc method and set in the "comments" section of the dataflip.json file.
        /// </summary>
        public string Comments { get; set; }
    }
}
