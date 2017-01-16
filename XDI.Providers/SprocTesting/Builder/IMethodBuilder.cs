using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Code.DbFirst.Discovery;
using XDI.CSharp.ComponentModel;

namespace XDI.Providers.SprocTesting.Builder
{
    public interface IMethodBuilder
    {
        CodeMethod Build(CodeContext context, SprocSettings settings);
    }
}
