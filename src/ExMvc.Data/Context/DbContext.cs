using ExMvc.Data.Context.Strategy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExMvc.Data.Context
{
    public class DbContext
    {
        private IDbStrategy _dbStrategy;

        public DbContext SetStrategy(string providerType)
        {
            switch(providerType)
            {
                case "SqlServer":
                    _dbStrategy = new SqlServerStrategy();
                        break;
                default:
                    throw new Exception("Provider não compativel");
            };
            return this;
        }

        public IDbConnection GetDbContext(string connectionString)
            => _dbStrategy.GetConnection(connectionString);
    }
}
