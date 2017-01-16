using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Code.DbFirst.Discovery;

namespace XDI.CSharp.ComponentModel
{
    public class CodeContext
    {
        public string ConnectionName { get; set; }
        public string ContextName { get; set; }
        public IEnumerable<CodeMethod> CodeMethods { get; set; } = new List<CodeMethod>();
        public string Namespace { get; set; }
        public string Filename { get; set; }
    }
}
