using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Core.Outputs;

namespace XDI.Code.Outputs
{
    public class ConsoleOutput : ICodeWriter
    {
        public void BeginCode(string contextName)
        {
            Console.WriteLine($"Writing... {contextName}");
        }

        public void Save(string filename)
        {
            Console.WriteLine($"File being saved in {filename}");
        }

        public void WriteCode(int indentationCount, string code)
        {
            for (int i = 0; i < indentationCount; i++)
                Console.Write("+");

            Console.WriteLine(code);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }
    }
}
