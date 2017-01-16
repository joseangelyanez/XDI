using System;
using System.Collections.Generic;
using XDI.Core.DependencyInjection;
using XDI.Core.Outputs;
using XDI.CSharp.ComponentModel;
using XDI.Generators.CSharp.CodeGeneration;

namespace XDI.Generators.CSharp
{
    public class DbContextCodeGenerator : ICodeGenerator
    {
        public CodeContext Context { get; private set; }

        private readonly CodeOutput _output = null;
        private readonly IEnumerable<ICodeGenerationChunk> _codeChunks = null;

        public DbContextCodeGenerator(
            CodeContext context, 
            CodeOutput output, 
            ICodeGenerationChunk[] codeChunks)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));
            if (codeChunks == null)
                throw new ArgumentNullException(nameof(codeChunks));

            _output = output;
            _codeChunks = codeChunks;

            Context = context;
        }

        public void GenerateCode()
        {
            _output.BeginCode(Context.ContextName);

            foreach (var chunk in _codeChunks)
            {
                chunk.Write(Context, _output);
            }

            _output.Save(Context.Filename);
        }
    }
}
