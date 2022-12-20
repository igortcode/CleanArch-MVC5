using System.Data;

namespace ExMvc.Data.Context.Strategy
{
    internal interface IDbStrategy
    {
        IDbConnection GetConnection(string connectionString);
    }
}
