using ExMvc.Domain.Entities;
using ExMvc.Domain.Interfaces.Repository.Generic;

namespace ExMvc.Domain.Interfaces.Repository.Specific
{
    public interface IEmpresaRepository : IGenericRepository<Empresa>
    {
        bool ExisteEmpresaComCNPJ(string cnpj);
        Empresa BuscarEmpresaPorCNPJ(string cnpj);
    }
}
