﻿using DigitalHealth.Web.Services;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DigitalHealth.Web.EntitiesDto;

namespace DigitalHealth.Web.Controllers
{


    public class AccountController : Controller
    {
        private readonly AccountService accountService = new AccountService();
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public async Task<JsonResult> LoginExist(string login)
        {
            var result = await accountService.LoginExist(login);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> Register(AccountRegisterDto dto)
        {
            var result = await accountService.Register(dto);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Login(AccountLoginDto dto)
        {
            var result = await accountService.Login(dto, AuthenticationManager);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Logout()
        {
            await accountService.Logout(AuthenticationManager);
            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> MyProfile()
        {
            return View();
        }

        public async Task<JsonResult> GetMyProfile()
        {
            var name = User.Identity.Name;
            var userid = await accountService.GetUserId(name);
            var result = await accountService.GetProfile(userid);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> UpdateProfile(ProfileDto dto)
        {
            await accountService.UpdateProfile(dto);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}