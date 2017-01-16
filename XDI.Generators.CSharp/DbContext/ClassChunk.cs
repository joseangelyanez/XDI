using System;
using XDI.Core.Outputs;
using XDI.CSharp.ComponentModel;

namespace XDI.Generators.CSharp.CodeGeneration
{
    public class ClassChunk : ICodeGenerationChunk
    {
        private ICodeGenerationChunk _next = null;
        private IDbCommandExecutionCodeWriter _commandExecution = null;

        public ClassChunk(ICodeGenerationChunk next, IDbCommandExecutionCodeWriter commandExecution)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            if (commandExecution == null)
                throw new ArgumentNullException(nameof(commandExecution));

            _next = next;
            _commandExecution = commandExecution;
        }

        public void Write(CodeContext model, CodeOutput output)
        {
            string contextName = model.ContextName;

            output.WriteCode($"public class {contextName} : DataflipContext");
            output.WriteCode("{");
            output.WriteCode($"public {contextName}(DataflipSettings settings)");
            output.WriteCode(": ");
            output.WriteCode("base(settings)");
            output.WriteCode("{}");
            output.WriteLine();
            output.WriteCode($"public {contextName}()");
            output.WriteCode(": ");
            output.WriteCode($"base(\"{contextName}\")");
            output.WriteCode("{}");

            /* Writes method execution. */
            _commandExecution.Write(model, output);

            _next.Write(model, output);

            output.WriteCode("}");
            output.WriteLine();
        }
    }
}
