using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Code.DbFirst.Discovery;
using XDI.Core.DependencyInjection;
using XDI.Core.Outputs;
using XDI.CSharp.ComponentModel;

namespace XDI.Generators.CSharp.CodeGeneration
{
    public class NamespaceChunk : ICodeGenerationChunk
    {
        private ICodeGenerationChunk _next;

        public NamespaceChunk(ICodeGenerationChunk next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));

            _next = next;
        }

        public void Write(CodeContext codeContext, CodeOutput output)
        {
            string @namespace = codeContext.Namespace;

            output.WriteCode($"namespace {@namespace}");
            output.WriteCode("{");
            _next.Write(codeContext, output);
            output.WriteCode("}");
        }
    }
}
