using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DigitalHealth.Web.Services;

namespace DigitalHealth.Web.Controllers
{
    [Authorize(Roles = "SysAdmin")]
    public class MarkController : Controller
    {
        MarkCRUDService markCrudService = new MarkCRUDService();
        // GET: Mark
        public async Task<ActionResult> Index(int? page)
        {
            int pageNumber = (page ?? 0);
            int pageSize = 10;
            var dbMarkList = await markCrudService.List(pageNumber, 10);

            return View(dbMarkList);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            await markCrudService.Delete(id);
            return null;
        }
    }
}