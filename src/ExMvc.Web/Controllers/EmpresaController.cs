using ExMvc.Application.DTO.Empresa;
using ExMvc.Application.DTO.Generic;
using ExMvc.Application.Enums;
using ExMvc.Application.Services.Interfaces;
using System.Web.Mvc;

namespace ExMvc.Web.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly IEmpresaServices _empresaServices;

        public EmpresaController(IEmpresaServices empresaServices)
        {
            _empresaServices = empresaServices;
        }

        public ActionResult Index(int pag = 1, int tmPag = 10)
        {
            var result = _empresaServices.BuscarPaginado(pag, tmPag);
            TempData["page"] = result.PageNumber;
            TempData["pageCount"] = result.PageCount;
            if(result.Response.Notification == NotificationEnum.Success)
                return View(result.Empresas);

            setNotification(result.Response);
            return View(result.Empresas);
        }

        [HttpGet]
        public ActionResult Create()
        {
         return View();
        }

        [HttpPost]
        public ActionResult Create(EmpresaDTO dto)
        {
            if(!ModelState.IsValid)
                return View(dto);
            
            var result = _empresaServices.Adicionar(dto);
            
            setNotification(result);
            if (result.Notification == NotificationEnum.Success)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var result = _empresaServices.BuscarPorId(id);
            if(result.Response.Notification == NotificationEnum.Success)
                return View(result);

            else
            {
                setNotification(result.Response);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public ActionResult Edit(EmpresaDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = _empresaServices.Atualizar(dto);
            
            setNotification(result);
            if (result.Notification == NotificationEnum.Success)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var result = _empresaServices.BuscarPorId(id);

            if (result.Response.Notification == NotificationEnum.Success)
            {
                return View(result);
            }
            else
            {
                setNotification(result.Response);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var result = _empresaServices.BuscarPorId(id);
            if (result.Response.Notification == NotificationEnum.Success)
            {
                return View(result);
            }
            else
            {
                setNotification(result.Response);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public ActionResult Delete(EmpresaDTO empresaDTO)
        {
            var result = _empresaServices.Excluir(empresaDTO.Identificador);

            if (result.Notification == NotificationEnum.Success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                setNotification(result);
                return RedirectToAction(nameof(Index));
            }           
        }

        private void setNotification(ResultDTO result)
        {
            switch (result.Notification)
            {
                case NotificationEnum.Success:
                    TempData["type"] = nameof(NotificationEnum.Success);
                    break;
                case NotificationEnum.Warning:
                    TempData["type"] = nameof(NotificationEnum.Warning);
                    break;
                case NotificationEnum.Error:
                    TempData["type"] = nameof(NotificationEnum.Error);
                    break;
            }

            TempData["message"] = result.Message;
        }
    }
}