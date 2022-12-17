using System.Collections.Generic;

namespace ExMvc.Domain.Interfaces.Repository.Generic
{
    public interface IDapperRepository
    {
        IList<T> DbQuery<T>(string sql, object param = null);
        T DbQueryFirst<T>(string sql, object param = null);
        T DPExecuteScalar<T>(string sql, object param = null);
    }
}
