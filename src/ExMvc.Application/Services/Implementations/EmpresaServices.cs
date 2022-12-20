using ExMvc.Application.DTO.Empresa;
using ExMvc.Application.DTO.Generic;
using ExMvc.Application.Services.Interfaces;
using ExMvc.Domain.CustomExceptions;
using ExMvc.Domain.Entities;
using ExMvc.Application.Enums;
using ExMvc.Domain.Interfaces.Repository.Specific;
using System;
using System.Collections.Generic;
using X.PagedList;

namespace ExMvc.Application.Services.Implementations
{
    public class EmpresaServices : IEmpresaServices
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaServices(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }
        public ResultDTO Adicionar(EmpresaDTO dto)
        {
            try
            {
                if (_empresaRepository.ExisteEmpresaComCNPJ(dto.CNPJ))
                    throw new DomainExceptionValidade("Já existe um empresa cadastrada com esse cnpj");

                var entidade = new Empresa(dto.NomeFantasia, dto.RazaoSocial, dto.CNPJ);
                _empresaRepository.Create(entidade);
                return new ResultDTO(NotificationEnum.Success, "Empresa cadastrada com sucesso!");
            }
            catch (DomainExceptionValidade dev)
            {
                return new ResultDTO(NotificationEnum.Warning, dev.Message);
            }
            catch(Exception)
            {
                return new ResultDTO(NotificationEnum.Error, "Ops, ocorreu alguma instabilidade no sistema e não foi possível executar a operação!");
            }

        }

        public ResultDTO Atualizar(EmpresaDTO dto)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Identificador))
                    throw new ArgumentException("Identificador inválido!");

                if (_empresaRepository.ExisteEmpresaComCNPJ(dto.CNPJ))
                    if(_empresaRepository.BuscarEmpresaPorCNPJ(dto.CNPJ).Id != dto.Identificador)
                        throw new DomainExceptionValidade("Já existe um empresa cadastrada com esse cnpj");
                
                var entidade = _empresaRepository.FindById(Guid.Parse(dto.Identificador));
                if (entidade == null)
                    throw new Exception("Não foi possível encontrar uma empresa com esse identificador");

                entidade.Atualizar(dto.NomeFantasia, dto.RazaoSocial, dto.CNPJ);
                _empresaRepository.Update(entidade);
                return new ResultDTO(NotificationEnum.Success, "Empresa atualizada com sucesso!");
            }
            catch (DomainExceptionValidade dev)
            {
                return new ResultDTO(NotificationEnum.Warning, dev.Message);
            }
            catch (ArgumentException ae)
            {
                return new ResultDTO(NotificationEnum.Error, ae.Message);
            }
            catch (Exception)
            {
                return new ResultDTO(NotificationEnum.Error, "Ops, ocorreu alguma instabilidade no sistema e não foi possível executar a operação!");
            }
        }

        public ListarEmpresaDTO BuscarPaginado(int pagina = 1, int tmPagina = int.MaxValue)
        {
            try
            {
                var result = _empresaRepository.FindAll().ToPagedList(pagina > 1 ? pagina : 1, tmPagina > 1 ? tmPagina : 10);
                return new ListarEmpresaDTO
                {
                    Empresas = mapToEmpresaDTO(result),
                    PageNumber = result.PageNumber,
                    PageCount = result.PageCount
                };
            }
            catch (Exception)
            {
                return new ListarEmpresaDTO
                {
                    Response = new ResultDTO(NotificationEnum.Error, "Ops, ocorreu alguma instabilidade no sistema e não foi possível executar a operação!")
                };
            }
        }
        
        private IEnumerable<EmpresaDTO> mapToEmpresaDTO(IPagedList<Empresa> result)
        {
            return result.Select(a => new EmpresaDTO
            {
                Identificador = a.Id.ToString(),
                CNPJ = a.CNPJ,
                NomeFantasia = a.NomeFantasia,
                RazaoSocial = a.RazaoSocial
            });
        }

        public BuscarEmpresaDTO BuscarPorId(string identificador)
        {
            try
            {
                if (!Guid.TryParse(identificador, out Guid id))
                    throw new ArgumentException("Identificador inválido!");

                var result = _empresaRepository.FindById(id);
                return new BuscarEmpresaDTO
                {
                    Identificador = result.Id.ToString(),
                    CNPJ = result.CNPJ,
                    NomeFantasia = result.NomeFantasia,
                    RazaoSocial = result.RazaoSocial
                };
            }
            catch (Exception)
            {
                return new BuscarEmpresaDTO
                {
                    Response = new ResultDTO(NotificationEnum.Error, "Ops, ocorreu alguma instabilidade no sistema e não foi possível executar a operação!")
                };
            }
        }

        public ResultDTO Excluir(string identificador)
        {
            try
            {
                if (!Guid.TryParse(identificador, out Guid id))
                    throw new ArgumentException("Identificador inválido!");

                var result = _empresaRepository.FindById(id);
                if (result == null)
                    throw new Exception("Não existe nenhuma empresa cadastrada com esse identificador");

                _empresaRepository.Delete(id);
                return new ResultDTO(NotificationEnum.Success, "Registro excluído com sucesso!");
            }
            catch (ArgumentException ae)
            {
                return new ResultDTO(NotificationEnum.Error, ae.Message);
            }
            catch(Exception)
            {
                return new ResultDTO(NotificationEnum.Error, "Ops, ocorreu alguma instabilidade no sistema e não foi possível executar a operação!");
            }

        }
    }
}
