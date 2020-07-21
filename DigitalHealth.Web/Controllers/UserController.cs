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
    [Authorize(Roles = "SysAdmin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService,
            IRoleService roleService)
        {
            _roleService = roleService;
            _userService = userService;
        }
        // GET: User
        public async Task<ActionResult> Index(int? page)
        {
            int pageNumber = (page ?? 0);
            int pageSize = 10;
            var dbuserList = await _userService.List(pageNumber, 10);

            return View(dbuserList);
        }

        public async Task<JsonResult> AllRoles()
        {
            var result = await _roleService.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SetRole(Guid UserId, Guid RoleId)
        {
            await _userService.SetUserRole(new UserDto {UserId = UserId}, RoleId);
            return null;
        }
    }
}