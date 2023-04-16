using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using SHUNetMVC.Abstraction.Services;
using System.Web.Security;
using System.Security.Principal;
using System.Web.Configuration;
using System.Threading.Tasks;
using SHUNetMVC.Abstraction.Model.Response;

namespace SHUNetMVC.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.Title = "Home Page";
                var task = Task.Run(async () => await _userService.GetCurrentUserInfo());
                User user = task.Result;
                return View("Index", user);
            }

            //if (IsSSOEnabled())
            //{
            //    return RedirectToAction("LoginSSO");
            //}

            return View("LoginForm");
        }

        private bool IsSSOEnabled()
        {
            AuthenticationSection authenticationSection = (AuthenticationSection)ConfigurationManager.GetSection("system.web/authentication");
            return authenticationSection.Mode == System.Web.Configuration.AuthenticationMode.None;
        }

        [Authorize]
        public ActionResult LoginSSO()
        {
            ViewBag.Title = "Home Page";
            var task = Task.Run(async () => await _userService.GetCurrentUserInfo());
            User user = task.Result;
            return View("Index", user);
        }

        [HttpGet]
        public bool LoginForm(string userName)
        {
            if (IsSSOEnabled())
                return false;

            var user = _userService.GetUserInfo(userName);
            if (user == null)
                return false;


            HttpContext.User = new GenericPrincipal(new GenericIdentity(userName), new string[] { "user" });
            FormsAuthentication.SetAuthCookie(userName, true);
            return true;
        }
    }
}
