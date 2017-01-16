using System.Linq;
using XDI.Code.DbFirst.Discovery;
using XDI.Core.Outputs;
using XDI.CSharp.ComponentModel;

namespace XDI.Generators.CSharp.CodeGeneration.Dataflip
{
    public class DataflipCommandExecutionCodeWriter : IDbCommandExecutionCodeWriter
    {
        public void Write(CodeContext context, CodeOutput code)
        {
            /* Methods */
            foreach (var method in context.CodeMethods)
            {
                code.WriteCode($"#region {method.Name}");
                
                if (method.Comments != null)
                {
                    code.WriteCode("///<summary>");
                    code.WriteCode($"///{ method.Comments }");
                    code.WriteCode("///</summary>");
                }

                if (method.MethodType == MethodTypes.Query )
                    code.WriteCode("public IEnumerable<{0}Result> {0}({1})",
                        method.Name,
                        method.Parameters.Values.Count == 0 ? "" : string.Format("{0}Parameters parameters", method.Name)
                    );

                if (method.MethodType == MethodTypes.NonQuery)
                    code.WriteCode("public int {0}({1})",
                        method.Name,
                        method.Parameters.Values.Count == 0 ? "" : string.Format("{0}Parameters parameters", method.Name)
                    );

                if (method.MethodType == MethodTypes.Scalar)
                    code.WriteCode("public {2} {0}({1})",
                        method.Name,
                        method.Parameters.Values.Count == 0 ? "" : string.Format("{0}Parameters parameters", method.Name),
                        method.ReturnType.Name
                    );

                code.WriteCode("{");
                if (method.MethodType == MethodTypes.Query)
                    code.WriteCode("return new SqlQuery(Settings).ExecuteObjectArray(");
                if (method.MethodType == MethodTypes.NonQuery)
                    code.WriteCode("return new SqlQuery(Settings).ExecuteNonQuery(");
                if (method.MethodType == MethodTypes.Scalar)
                    code.WriteCode($"return ({method.ReturnType.Name}) new SqlQuery(Settings).ExecuteScalar(");

                //code.BeingIndentation();
                code.WriteCode($"query : \"{method.BoundTo}\",");
                code.WriteCode("parameters : _ =>");
                code.WriteCode("{");
                code.WriteCode("if (parameters == null) return;");
                code.WriteLine();

                foreach (var param in method.Parameters.Values)
                {
                    code.WriteCode($"_.AddWithValue(\"{param.Name}\", parameters.{param.Name.Replace("@", "")});");
                }
                if (method.MethodType == MethodTypes.Query)
                {
                    code.WriteCode("},");
                    code.WriteCode($"mapping: reader => new {method.Name}Result()");
                    code.WriteCode("{");

                    int counter = 0;
                    foreach (var param in method.ResultProperties)
                    {
                        counter++;
                        code.WriteCode("{0} = reader[\"{0}\"] as {1}{2}",
                            param.Name,
                            param.PropertyType.IsValueType? $"{param.PropertyType.Name}?" : param.PropertyType.Name,
                            counter < method.ResultProperties.Count() ? "," : "");
                    }
                    code.WriteCode("}");
                }
                else
                {
                    code.WriteCode("}");
                }
                //code.EndIndentation();
                code.WriteCode(");");
                code.WriteCode("}");
                code.WriteCode("#endregion");
                code.WriteLine();
            }
            
        }
    }
}