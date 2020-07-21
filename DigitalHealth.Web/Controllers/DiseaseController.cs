using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DigitalHealth.Web.EntitiesDto;
using DigitalHealth.Web.Services;

namespace DigitalHealth.Web.Controllers
{
    [Authorize(Roles = "SysAdmin, Expert")]
    public class DiseaseController : Controller
    {
        private readonly DiseaseCRUDService _diseaseCrudService = new DiseaseCRUDService();
        // GET: ICD
        public async Task<ActionResult> Index(int? page)
        {
            int pageNumber = (page ?? 0);
            int pageSize = 10;
            var dbICDList = await _diseaseCrudService.List(pageNumber, 10);

            return View(dbICDList);
        }

        [HttpPost]
        public async Task<ActionResult> Create(DiseaseCreateDto item)
        {
            DiseaseCreateDto disease = item;
            await _diseaseCrudService.Create(disease);

            return null;
        }
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _diseaseCrudService.Delete(id);
            return null;
        }
        [HttpPost]
        public async Task<ActionResult> Update(DiseaseUpdateDto dto)
        {
            await _diseaseCrudService.Update(dto);
            return null;
        }
        [HttpGet]
        public async Task<JsonResult> Get(Guid id)
        {
            var item = await _diseaseCrudService.Get(id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var item = await _diseaseCrudService.GetAll();
            return Json(item, JsonRequestBehavior.AllowGet);
        }
    }
}