using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ExMvc.Application.DTO.Empresa
{
    public class EmpresaDTO
    {
        [Key]
        [DisplayName("Identificador")]
        public string Identificador { get; set; }
        [Required(ErrorMessage = "Campo Nome Fantasia obrigatório!")]
        [DisplayName("Nome Fantasia")]
        public string NomeFantasia { get; set; }
        [Required(ErrorMessage = "Campo razão social obrigatório!")]
        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }
        [Required(ErrorMessage = "Campo CNPJ obrigatório!")]
        [DisplayName("CNPJ")]
        public string CNPJ { get; set; }
    }
}
