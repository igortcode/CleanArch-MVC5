using ExMvc.Application.DTO.Generic;
using System.Collections.Generic;

namespace ExMvc.Application.DTO.Empresa
{
    public class ListarEmpresaDTO
    {
        public IEnumerable<EmpresaDTO> Empresas { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }

        public ResultDTO Response { get; set; }   
    }
}
