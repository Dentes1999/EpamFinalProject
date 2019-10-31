using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBll.Models;
using Microsoft.AspNet.Identity;
using MyRepository.Models;

namespace EpamFinalProject.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly IBll _myBll;

        public ApartmentsController(IBll cont)
        {
            _myBll = cont;
        }
        // GET: Apartments
        public ActionResult Apartments()
        {
            List<ApartmentView> t;
            if (User.IsInRole("Admin"))
            {
                t = _myBll.GetAllViewApartments().ToList();
            }
            else
            {
                t=_myBll.GetAllAppliableViewApartments().ToList();
            }
            
            return View(t);
        }

        [HttpPost]
        public ActionResult QueryApartments(QueryView qv)
        {
            var res=_myBll.GetSpecialApartments(qv);
            return PartialView("QueryAp",res);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add(ApartmentEditor ae)
        {
            if (HttpContext.Request.HttpMethod == "POST")
            {
                try
                {


                    if (ae.Href2 != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Content/Photos"), Path.GetFileName(ae.Href2.FileName));
                        ae.Href2.SaveAs(path);
                        ae.Href1 = "~/Content/Photos/" + Path.GetFileName(ae.Href2.FileName);

                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }
                if (ae.Forcheck != null && ae.Forcheck.Length == 1) ae.NotAppliable = true;
                else ae.NotAppliable = false;
                _myBll.AddApartment(ae);
                return View();
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string ToEdit)
        {
            ApartmentEditor a = _myBll.GetExactApartmentEditor(ToEdit);
            
            return View(a);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult EditInfo(ApartmentEditor ae)
        {
            if (HttpContext.Request.HttpMethod == "POST")
            {
                try
                {


                    if (ae.Href2!=null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Content/Photos"), Path.GetFileName(ae.Href2.FileName));
                        ae.Href2.SaveAs(path);
                        ae.Href1 = "~/Content/Photos/" + Path.GetFileName(ae.Href2.FileName);

                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }

                if (ae.Forcheck != null && ae.Forcheck.Length == 1) ae.NotAppliable = true;
                else ae.NotAppliable = false;
                _myBll.EditApartment(ae);
                return View("Edit",ae);
            }
            return View("Edit", ae);
        }

        public ActionResult Book(string ToBook)
        {
            Apartment a = _myBll.GetExactApartment(ToBook);
            List<string> list=new List<string>();
            a.GetReservedDates().ForEach(f=>list.Add(f.ToString("yyyy-MM-dd")));
            var res=list.ToArray();

            ViewBag.Dateses = res;
            return View(a);
        }
        [HttpPost]
        public ActionResult Application(StartFormViewModel qr)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {

                var script = "window.location ='" + Url.Action("Login", "Account") + "' ;";
                return JavaScript(script);
            }
            qr.UserId = User.Identity.GetUserId();
            _myBll.AddApplication(qr);
            return RedirectToAction("Book",new {ToBook=qr.ApId});
        }

    }
}