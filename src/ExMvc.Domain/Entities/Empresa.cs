using ExMvc.Domain.CustomExceptions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExMvc.Domain.Entities
{
    [Table("Empresa")]
    public class Empresa : Entity
    {
        public string NomeFantasia { get; private set; }
        public string RazaoSocial { get; private set; }
        public string CNPJ { get; private set; }

        public Empresa(string nomeFantasia, string razaoSocial, string cNPJ)
        {
            validarCampos(nomeFantasia, razaoSocial, cNPJ);
            
            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            CNPJ = cNPJ;
        }

        public Empresa(string id, string nomeFantasia, string razaoSocial, string cNPJ, DateTime dataCadastro)
        {
            validarCampos(nomeFantasia, razaoSocial, cNPJ);

            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            CNPJ = cNPJ;
            Id = id;
            DataCadastro = dataCadastro;    
        }

        public void Atualizar(string nomeFantasia, string razaoSocial, string cNPJ)
        {
            validarCampos(nomeFantasia, razaoSocial, cNPJ);

            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            CNPJ = cNPJ;
        }

        private void validarCampos(string nomeFantasia, string razaoSocial, string cNPJ)
        {
            DomainExceptionValidade.When(string.IsNullOrEmpty(nomeFantasia), "Nome Fantasia é obrigatório");
            DomainExceptionValidade.When(nomeFantasia.Length < 5, "Nome fantasia muito curto. O campo deve ter mais de 4 caracteres");

            DomainExceptionValidade.When(string.IsNullOrEmpty(razaoSocial), "Razao Social é obrigatório");
            DomainExceptionValidade.When(razaoSocial.Length < 5, "Razao Social muito curto. O campo deve ter mais de 4 caracteres");

            DomainExceptionValidade.When(string.IsNullOrEmpty(cNPJ), "CNPJ é obrigatório");
            DomainExceptionValidade.When(cNPJ.Length < 18, "CNPJ inválido. O campo deve ter 18 caracteres");
            DomainExceptionValidade.When(cNPJ.Length > 18, "CNPJ inválido. O campo deve ter 18 caracteres");
            //DomainExceptionValidade.When(validaCNPJ(cNPJ), "CNPJ inválido");
        }

        private static bool validaCNPJ(string vrCNPJ)
        {
            string CNPJ = vrCNPJ.Replace(".", "");

            CNPJ = CNPJ.Replace("/", "");

            CNPJ = CNPJ.Replace("-", "");

            int[] digitos, soma, resultado;

            int nrDig;

            string ftmt;

            bool[] CNPJOk;

            ftmt = "6543298765432";

            digitos = new int[14];

            soma = new int[2];

            soma[0] = 0;

            soma[1] = 0;

            resultado = new int[2];

            resultado[0] = 0;

            resultado[1] = 0;

            CNPJOk = new bool[2];

            CNPJOk[0] = false;

            CNPJOk[1] = false;



            try

            {

                for (nrDig = 0; nrDig < 14; nrDig++)

                {

                    digitos[nrDig] = int.Parse(

                        CNPJ.Substring(nrDig, 1));

                    if (nrDig <= 11)

                        soma[0] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig + 1, 1)));

                    if (nrDig <= 12)

                        soma[1] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig, 1)));

                }

                for (nrDig = 0; nrDig < 2; nrDig++)

                {

                    resultado[nrDig] = (soma[nrDig] % 11);

                    if ((resultado[nrDig] == 0) || (

                         resultado[nrDig] == 1))

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == 0);

                    else

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == (

                        11 - resultado[nrDig]));

                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch

            {
              return false;
            }

        }

    }
}
