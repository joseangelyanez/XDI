using System;
using System.Linq;
using XDI.Code.DbFirst.Discovery;
using XDI.Core.DependencyInjection;
using XDI.Core.Outputs;
using XDI.CSharp.ComponentModel;

namespace XDI.Generators.CSharp.CodeGeneration
{
    public class ResultClassesChunk : ICodeGenerationChunk
    {
        public void Write(CodeContext context, CodeOutput output)
        {
            var queryMethods =
                context.CodeMethods.Where(n => n.MethodType == MethodTypes.Query);

            /* No query methods. */
            if (queryMethods.FirstOrDefault() == null)
                return;

            /* Result Classes */
            output.WriteCode("#region Result Classes");
            foreach (var method in queryMethods)
            {
                output.WriteCode($"public class {method.Name}Result");
                output.WriteCode("{");
                if (method.ResultProperties != null)
                {
                    foreach (var property in method.ResultProperties)
                    {
                        string typeName =
                            property.PropertyType.IsValueType ? 
                                $"{property.PropertyType.Name}?" 
                                : 
                                property.PropertyType.Name;

                        output.WriteCode($"public { typeName } {property.Name} {{ get; set; }}");
                    }
                }
                if (method.UnboundProperties != null)
                {
                    foreach (var property in method.UnboundProperties)
                    {
                        output.WriteCode($"public {property.PropertyType.Name} {property.Name} {{ get; set; }}");
                    }
                }
                output.WriteCode("}");
                output.WriteLine();
            }

            output.WriteCode("#endregion");
            output.WriteLine();
        }
    }
}
