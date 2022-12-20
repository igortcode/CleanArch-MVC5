using Dapper;
using ExMvc.Data.Repository.Generic;
using ExMvc.Domain.Entities;
using ExMvc.Domain.Interfaces.Options;
using ExMvc.Domain.Interfaces.Repository.Specific;
using ExMvc.Domain.Settings;


namespace ExMvc.Data.Repository.Specific
{
    public class EmpresaRepository : GenericRepository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(IOptions<DbSettings> options) : base(options)
        {  
        }

        public bool ExisteEmpresaComCNPJ(string cnpj)
        {
            var sql = "SELECT COUNT(1) FROM Empresa WHERE CNPJ = @Cnpj";
            var result = DbConnection.ExecuteScalar<int>(sql, new {Cnpj = cnpj});
            return result > 0;
        }

        public Empresa BuscarEmpresaPorCNPJ(string cnpj)
        {
            var sql = "SELECT * FROM Empresa WHERE CNPJ = @Cnpj";
            var result = DbConnection.QueryFirst<Empresa>(sql, new {Cnpj = cnpj});
            return result;
        }
    }
}
