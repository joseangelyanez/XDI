using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace XDI.Providers.SprocTesting.Settings
{
    public class ContextSettings
    {
        public string ConnectionString { get; internal set; }
        public string ContextFilename { get; internal set; }
        public string ContextName { get; internal set; }
        public string Namespace { get; internal set; }
        public IEnumerable<SprocSettings> SprocSettings { get; set; }
    }
}