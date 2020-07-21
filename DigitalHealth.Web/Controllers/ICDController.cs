using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Helpers;
using Antlr.Runtime;
using DigitalHealth.Web.Entities;
using DigitalHealth.Web.EntitiesDto;
using DigitalHealth.Web.Services;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PagedList;

namespace DigitalHealth.Web.Controllers
{
    public class ICDController : Controller
    {
        private  readonly ICDCRUDService _icdcrudService = new ICDCRUDService();
        // GET: ICD
        public async Task<ActionResult> Index(int? page)
        {
            int pageNumber = (page ?? 0);
            int pageSize = 10;
            List<ICDDto> ICDs = new List<ICDDto>();
            var dbICDList = await _icdcrudService.List(pageNumber, 10);

            return View(dbICDList);
        }

        [HttpGet]
        public async Task<JsonResult> Get(Guid Id)
        {
            var item = await _icdcrudService.Get(Id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var result = await _icdcrudService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Update(ICDDto item)
        {
            await _icdcrudService.Update(item);
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            var id = new Guid(Id);
            await _icdcrudService.Delete(id);
            return null;
        }
        [HttpPost]
        public async Task<ActionResult> Create(ICDDto item)
        {
            ICDDto icd = item;
            await _icdcrudService.Create(icd);
            return null;
        }
    }
}