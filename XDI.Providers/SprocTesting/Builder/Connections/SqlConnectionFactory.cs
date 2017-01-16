using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XDI.CSharp.ComponentModel;

namespace XDI.Providers.SprocTesting.Builder
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly Dictionary<object, SqlConnection> _connections = new Dictionary<object, SqlConnection>();

        public DbConnection CreateConnection(CodeContext context)
        {
            SqlConnection connection = null;

            if (_connections.ContainsKey(context))
                return _connections[context];

            /* Creates the connection. */
            connection = new SqlConnection(context.ConnectionName);
            connection.Open();

            _connections.Add(context, connection);

            return connection;
        }

        public void Dispose()
        {
            foreach (var connection in _connections.Values)
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();

                connection.Dispose();
            }
        }
    }
}
