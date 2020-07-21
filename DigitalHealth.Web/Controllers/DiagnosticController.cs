using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DigitalHealth.GlobalInterfaces;
using DigitalHealth.Services;
using DigitalHealth.Web.EntitiesDto;
using DigitalHealth.Web.Services;

namespace DigitalHealth.Web.Controllers
{
    [Authorize]
    public class DiagnosticController : Controller
    {
        private readonly IDiagnosticService _diagnosticService;
        private readonly ISymptomCRUDService _symptomCrudService;
        private readonly IMarkCRUDService _markCrudService;
        private readonly  IAccountService _accountService;

       public DiagnosticController()
       {
           _diagnosticService = new DiagnosticService();
           _symptomCrudService = new SymptomCRUDService();
           _markCrudService = new MarkCRUDService();
           _accountService = new AccountService();
       }
        // GET: Diagnostic
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Symptoms()
        {
            var result = await _symptomCrudService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Diagnostic(List<Guid> SymptomIds)
        {
            var result = await _diagnosticService.GetResult(SymptomIds);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> FindMethods(Guid DiseaseId)
        {
            var result = await _diagnosticService.FindMethods(DiseaseId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMark(MarkDto dto)
        {
            var currentuserName = User.Identity.Name;
            var currentuserId =await _accountService.GetUserId(currentuserName);
            dto.UserId = currentuserId;
            await _markCrudService.Create(dto);
            return null;
        }

        [HttpGet]
        public async Task<JsonResult> GetMarks(Guid MethodId)
        {
            var marks = await _markCrudService.GetByMethod(MethodId);
            return Json(marks, JsonRequestBehavior.AllowGet);
        }
    }
}