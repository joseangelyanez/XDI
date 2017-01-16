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
    public interface ICodeGenerationChunk
    {
        void Write(CodeContext model, CodeOutput output);
    }
}
