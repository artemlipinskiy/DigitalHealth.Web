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
    [Authorize(Roles = "SysAdmin")]
    public class UserController : Controller
    {
        private readonly UserService userService = new UserService();
        private readonly RoleService roleService = new RoleService();
        // GET: User
        public async Task<ActionResult> Index(int? page)
        {
            int pageNumber = (page ?? 0);
            int pageSize = 10;
            var dbuserList = await userService.List(pageNumber, 10);

            return View(dbuserList);
        }

        public async Task<JsonResult> AllRoles()
        {
            var result = await roleService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SetRole(Guid UserId, Guid RoleId)
        {
            await userService.SetUserRole(new UserDto {UserId = UserId}, RoleId);
            return null;
        }
    }
}