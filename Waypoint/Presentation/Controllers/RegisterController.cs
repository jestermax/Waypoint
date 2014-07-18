﻿using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using Domain.Authentication;
using Domain.Dto.Inbound;

namespace Presentation.Controllers
{
    public class RegisterController : Controller
    {
        private ApplicationUserManager applicationUserManager;

        public RegisterController()
        { }

        public RegisterController(ApplicationUserManager applicationUserManager)
        {
            this.applicationUserManager = applicationUserManager;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email
                };

                IdentityResult result = await UserManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(registerDto);
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = isPersistent
            }, await user.GenerateUserIdentityAsync(UserManager));
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return applicationUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                applicationUserManager = value;
            }
        }
    }
}