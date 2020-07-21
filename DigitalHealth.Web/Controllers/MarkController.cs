using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DigitalHealth.GlobalInterfaces;
using DigitalHealth.Web.Services;

namespace DigitalHealth.Web.Controllers
{
    [Authorize(Roles = "SysAdmin")]
    public class MarkController : Controller
    {
        private IMarkCRUDService _markCrudService;
        // GET: Mark
        public MarkController(IMarkCRUDService markCrudService)
        {
            _markCrudService = markCrudService;
        }
        public async Task<ActionResult> Index(int? page)
        {
            int pageNumber = (page ?? 0);
            int pageSize = 10;
            var dbMarkList = await _markCrudService.List(pageNumber, 10);

            return View(dbMarkList);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _markCrudService.Delete(id);
            return null;
        }
    }
}