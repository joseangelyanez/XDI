using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XDI.Core.Outputs
{
    public interface ICodeWriter
    {
        void BeginCode(string contextName);
        void WriteCode(int indentationCount, string code);
        void WriteLine();
        void Save(string filename);
    }
}
