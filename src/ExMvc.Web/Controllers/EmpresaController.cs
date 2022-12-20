using ExMvc.Application.DTO.Empresa;
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

            if (result.Notification == NotificationEnum.Success)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var result = _empresaServices.BuscarPorId(id);

            return View(result);
        }

        [HttpPost]
        public ActionResult Edit(EmpresaDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = _empresaServices.Atualizar(dto);

            if (result.Notification == NotificationEnum.Success)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            var result = _empresaServices.BuscarPorId(id);

            return View(result);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var result = _empresaServices.BuscarPorId(id);

            return View(result);
        }

        [HttpPost]
        public ActionResult Delete(EmpresaDTO empresaDTO)
        {
            var result = _empresaServices.Excluir(empresaDTO.Identificador);

            if (result.Notification == NotificationEnum.Success)
                return RedirectToAction(nameof(Index));

            var dto = _empresaServices.BuscarPorId(empresaDTO.Identificador);
            return View(dto);
        }
    }
}