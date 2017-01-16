using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.Code;
using XDI.Code.DbFirst.Discovery;
using XDI.CSharp.ComponentModel;

namespace XDI.Providers.SprocTesting.Builder
{
    public class SqlCodeMethodBuilder : IMethodBuilder
    {
        public readonly IDbConnectionFactory _connectionFactory;
        private readonly TypeResolver _typeResolver;

        public SqlCodeMethodBuilder(IDbConnectionFactory connectionFactory, TypeResolver typeResolver)
        {
            if (typeResolver == null)
                throw new ArgumentNullException(nameof(typeResolver));
            if (connectionFactory == null)
                throw new ArgumentNullException(nameof(connectionFactory));

            _connectionFactory = connectionFactory;
            _typeResolver = typeResolver;
        }

        public CodeMethod Build(CodeContext context, SprocSettings settings)
        {
            CodeMethod codeMethod = new CodeMethod();

            /* Properties from settings. */
            codeMethod.BoundTo = settings.Name;
            codeMethod.Comments = settings.Comments;
            codeMethod.Name = settings.Method ?? settings.Name;

            SqlConnection connection = _connectionFactory.CreateConnection(context) as SqlConnection;
            if (connection == null)
                throw new InvalidOperationException($"Could not retrieve a SqlConnection for context '{context.ContextName}'");

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = settings.Name;

                SqlCommandBuilder.DeriveParameters(command);

                foreach (SqlParameter sqlParameter in command.Parameters)
                {
                    if (sqlParameter.Direction == System.Data.ParameterDirection.Output
                        ||
                        sqlParameter.Direction == System.Data.ParameterDirection.ReturnValue)
                        continue;

                    codeMethod.Parameters.Add(
                        sqlParameter.ParameterName,
                        new CodeParameter()
                        {
                            Name = sqlParameter.ParameterName,
                            ParameterType = _typeResolver.GetClrTypeFromParameter(sqlParameter)
                        }
                    );
                }

                if (settings.TestParams == null)
                {
                    codeMethod.ReturnType = typeof(int);
                    codeMethod.MethodType = MethodTypes.NonQuery;

                    /* Returns without testing the sproc. */
                    return codeMethod;
                }
               
                foreach (var param in settings.TestParams)
                {
                    var sqlParam = command.Parameters[param.ParameterName];
                    sqlParam.Value =
                        Convert.ChangeType(
                                param.TestWith,
                                param.TestAs
                            );
                }
                /*
                    GlobalProgress.NotifyProgress($"Testing sproc '{section.Name}' with {command.Parameters.Count} parameter(s) ...");
                */

                /* Tests the sproc. */
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.FieldCount > 1)
                    {
                        List<CodeProperty> properties = new List<CodeProperty>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            CodeProperty sprocProperty = new CodeProperty();
                            sprocProperty.PropertyType = reader.GetFieldType(i);
                            sprocProperty.Name = reader.GetName(i);
                            sprocProperty.BoundTo = reader.GetName(i);

                            properties.Add(sprocProperty);
                            codeMethod.MethodType = MethodTypes.Query;
                        }
                        codeMethod.ResultProperties = properties;

                        /*
                            GlobalProgress.NotifyProgress($"Added '{unboundProperty.Name}' as an unbound property to ${method.MethodName}");
                            GlobalProgress.NotifyProgress($"'{section.Name}' returned multiple columns, generating result class.");
                        */
                    }

                    if (reader.FieldCount == 1)
                    {
                        /*
                            GlobalProgress.NotifyProgress($"'{section.Name}' returned one column, generating method as scalar.");
                        */
                        codeMethod.ReturnType = reader.GetFieldType(0);
                        codeMethod.MethodType = MethodTypes.Scalar;
                    }

                    if (reader.FieldCount == 0)
                    {
                        /*
                            GlobalProgress.NotifyProgress($"'{section.Name}' didn't return any columns, returning System.Int32.");
                        */
                        codeMethod.MethodType = MethodTypes.NonQuery;
                        codeMethod.ReturnType = typeof(int);
                    }

                    return codeMethod;
                }
            }
        }
    }
}
