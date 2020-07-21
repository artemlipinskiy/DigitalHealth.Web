using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DigitalHealth.GlobalInterfaces;
using DigitalHealth.Web.EntitiesDto;
using DigitalHealth.Web.Services;

namespace DigitalHealth.Web.Controllers
{
    [Authorize(Roles = "SysAdmin, Expert")]
    public class MethodOfTreatmentController : Controller
    {
        private readonly IMethodOfTreatmentCRUDService _methodOfTreatmentCrudService;

        public MethodOfTreatmentController()
        {
            _methodOfTreatmentCrudService = new MethodOfTreatmentCRUDService();
        }
        // GET: MethodOfTreatment
        public async Task<ActionResult> Index(int? page)
        {
            int pageNumber = (page ?? 0);
            int pageSize = 10;
            var methodList = await _methodOfTreatmentCrudService.List(pageNumber, 10);

            return View(methodList);
        }

        [HttpPost]
        public async Task<ActionResult> Create(MethodOfTreatmentCreateDto item)
        {
            MethodOfTreatmentCreateDto method = item;
            await _methodOfTreatmentCrudService.Create(method);
            return null;
        }
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _methodOfTreatmentCrudService.Delete(id);
            return null;
        }

        [HttpPost]
        public async Task<JsonResult> Update(MethodOfTreatmentUpdateDto dto)
        {
            await _methodOfTreatmentCrudService.Update(dto);
            return null;
        }

        [HttpGet]
        public async Task<JsonResult> Get(Guid id)
        {
            var item = await _methodOfTreatmentCrudService.Get(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}