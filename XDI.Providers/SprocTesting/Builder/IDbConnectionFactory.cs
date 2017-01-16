using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.CSharp.ComponentModel;

namespace XDI.Providers.SprocTesting.Builder
{
    public interface IDbConnectionFactory : IDisposable
    {
        DbConnection CreateConnection(CodeContext context);
    }
}
