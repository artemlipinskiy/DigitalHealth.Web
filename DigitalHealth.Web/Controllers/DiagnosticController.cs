using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DigitalHealth.Web.EntitiesDto;
using DigitalHealth.Web.Services;

namespace DigitalHealth.Web.Controllers
{
    [Authorize]
    public class DiagnosticController : Controller
    {
        private readonly DiagnosticService diagnosticService = new DiagnosticService();
        private readonly SymptomCRUDService symptomCrudService = new SymptomCRUDService();
        private readonly MarkCRUDService markCrudService = new MarkCRUDService();
       private readonly  AccountService accountService = new AccountService();
        // GET: Diagnostic
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> Symptoms()
        {
            var result = await symptomCrudService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Diagnostic(List<Guid> SymptomIds)
        {
            var result = await diagnosticService.GetResult(SymptomIds);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> FindMethods(Guid DiseaseId)
        {
            var result = await diagnosticService.FindMethods(DiseaseId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateMark(MarkDto dto)
        {
            var currentuserName = User.Identity.Name;
            var currentuserId =await accountService.GetUserId(currentuserName);
            dto.UserId = currentuserId;
            await markCrudService.Create(dto);
            return null;
        }

        [HttpGet]
        public async Task<JsonResult> GetMarks(Guid MethodId)
        {
            var marks = await markCrudService.GetByMethod(MethodId);
            return Json(marks, JsonRequestBehavior.AllowGet);
        }
    }
}