using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Code;
using XDI.CSharp.ComponentModel;
using XDI.Providers.SprocTesting.Settings;

namespace XDI.Providers.SprocTesting.Builder
{
    public class CodeContextBuilder
    {
        public ContextSettings ContextSettings { get; private set; }
        private readonly TypeResolver _typeResolver = null;
        private readonly IMethodBuilder _methodBuilder;

        public CodeContextBuilder(ContextSettings contextSettings, TypeResolver typeResolver, IMethodBuilder methodBuilder)
        {
            if (contextSettings == null)
                throw new ArgumentNullException(nameof(contextSettings));
            if (typeResolver == null)
                throw new ArgumentNullException(nameof(typeResolver));
            if (methodBuilder == null)
                throw new ArgumentNullException(nameof(methodBuilder));

            ContextSettings = contextSettings;
            _typeResolver = typeResolver;
            _methodBuilder = methodBuilder;
        }

        public CodeContext Build()
        {
            var context = new CodeContext()
            {
                Namespace       = ContextSettings.Namespace,
                ContextName     = ContextSettings.ContextName,
                ConnectionName  = ContextSettings.ConnectionString,
                Filename        = ContextSettings.ContextFilename,
            };

            context.CodeMethods =
                ContextSettings
                    .SprocSettings
                    .Select(sprocSettings => _methodBuilder.Build(context, sprocSettings))
                    .ToArray(); /* <- Forces the IEnumerable to execute immediately. */

            return context;
        }
    }
}
