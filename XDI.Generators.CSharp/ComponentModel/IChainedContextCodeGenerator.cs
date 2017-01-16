using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Core.DependencyInjection;

namespace XDI.CSharp.ComponentModel
{
    public interface IChainedContextCodeGenerator
    {
        IEnumerable<ICodeGenerator> GetGenerators(CodeContext context);
    }
}
