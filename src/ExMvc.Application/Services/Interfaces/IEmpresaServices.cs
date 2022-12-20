using ExMvc.Application.DTO.Empresa;
using ExMvc.Application.DTO.Generic;

namespace ExMvc.Application.Services.Interfaces
{
    public interface IEmpresaServices
    {
        ResultDTO Adicionar(EmpresaDTO dto);
        ResultDTO Atualizar(EmpresaDTO dto);
        ResultDTO Excluir(string identificador);
        BuscarEmpresaDTO BuscarPorId(string identificador);
        ListarEmpresaDTO BuscarPaginado(int pagina = 1, int tmPagina = int.MaxValue);
    }
}
