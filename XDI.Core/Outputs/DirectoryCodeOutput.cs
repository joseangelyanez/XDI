using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Core.DependencyInjection;

namespace XDI.Core.Outputs
{
    public class DirectoryCodeOutput : ICodeWriter
    {
        public StringWriter Code { get; private set; } = new StringWriter();

        public void BeginCode(string contextName)
        {}

        /// <summary>
        /// Writes a new File
        /// </summary>
        public void Save(string filename)
        {
            using (Stream stream = File.Create(filename))
            using(StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(Code.ToString());
            }
        }

        public void WriteCode(int indentationCount, string code)
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));

            Code.WriteLine(
                "{0}{1}",
                new String('\t', indentationCount),
                code
            );
        }

        public void WriteLine()
        {
            Code.WriteLine();
        }
    }
}
