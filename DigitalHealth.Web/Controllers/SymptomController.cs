using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DigitalHealth.Web.EntitiesDto;
using DigitalHealth.Web.Services;

namespace DigitalHealth.Web.Controllers
{
    [Authorize(Roles = "SysAdmin, Expert")]
    public class SymptomController : Controller
    {
        private readonly SymptomCRUDService _symptomCrudService = new SymptomCRUDService();
        // GET: ICD
        public async Task<ActionResult> Index(int? page)
        {
            int pageNumber = (page ?? 0);
            int pageSize = 10;
            var dbList = await _symptomCrudService.List(pageNumber, 10);

            return View(dbList);
        }

        [HttpGet]
        public async Task<JsonResult> Get(string Id)
        {
            var id = new Guid(Id);
            var item = await _symptomCrudService.Get(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var symptoms = await _symptomCrudService.GetAll();
            return Json(symptoms, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public async Task<JsonResult> GetDetails(string Id)
        {
            var id = new Guid(Id);
            var item = await _symptomCrudService.GetWithDiseases(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Update(SymptomUpdateDto item)
        {
            await _symptomCrudService.Update(item);
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            var id = new Guid(Id);
            await _symptomCrudService.Delete(id);
            return null;
        }
        [HttpPost]
        public async Task<ActionResult> Create(SymptomCreateDto item)
        {
            SymptomCreateDto symptom = item;
            await _symptomCrudService.Create(symptom);

                return null;
        }
    }
}