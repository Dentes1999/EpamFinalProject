using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EpamFinalProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Ninject;
using MyBll.Models;


namespace EpamFinalProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBll _myBll;

        public HomeController(IBll bll)
        {
            _myBll = bll;
        }
        public ActionResult Index()
        {
            return View();
        }

        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
        public ActionResult StartForm(StartFormViewModel st)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {

                var script = "window.location ='" + Url.Action("Login", "Account") + "' ;";
                return JavaScript(script);
            }

            st.UserId = User.Identity.GetUserId();
            _myBll.AddApplicationForAny(st);
            return PartialView("TestView",st.Class);
        }
        public ActionResult UserPerm()
        {
            var userRoles = new List<RolesViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager=new UserManager<ApplicationUser>(userStore);
            foreach (var user in userStore.Users.ToList())
            {
                var r = new RolesViewModel
                {
                    UserName=user.UserName,
                    UserId=user.Id
                };
                if (userManager.IsInRole(user.Id, "Admin")) r.IsAdmin = true;
                if (userManager.IsInRole(user.Id, "Manager")) r.IsManager = true;
                if(User.Identity.GetUserId()!=r.UserId && r.UserName!="dteslenko777@gmail.com")
                    userRoles.Add(r);
            }
            return View(userRoles);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> NewPermAsync(List<RolesViewModel> users)
        {
            var userRoles = new List<RolesViewModel>();
            var context = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            foreach (var user in users)
            {
                if (user.ForCheckAdmin != null && user.ForCheckAdmin.Length == 1) user.IsAdmin = true;
                else user.IsAdmin = false;
                if (user.ForCheckManager != null && user.ForCheckManager.Length == 1) user.IsManager = true;
                else user.IsManager = false;
                if (userManager.IsInRole(user.UserId, "Admin") && user.IsAdmin == false)
                    await userManager.RemoveFromRoleAsync(user.UserId, "Admin");
                if (!userManager.IsInRole(user.UserId, "Admin") && user.IsAdmin == true)
                    await userManager.AddToRoleAsync(user.UserId, "Admin");

                if (userManager.IsInRole(user.UserId, "Manager") && user.IsManager == false)
                    await userManager.RemoveFromRoleAsync(user.UserId, "Manager");
                if (!userManager.IsInRole(user.UserId, "Manager") && user.IsManager == true)
                    await userManager.AddToRoleAsync(user.UserId, "Manager");


                
            }
            return View("UserPerm",users);
        }


    }
}