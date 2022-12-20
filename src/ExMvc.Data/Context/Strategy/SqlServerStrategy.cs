using System.Data.SqlClient;
using System.Data;

namespace ExMvc.Data.Context.Strategy
{
    public class SqlServerStrategy : IDbStrategy
    {
        public IDbConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
