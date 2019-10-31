using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EpamFinalProject.Models;
using MyBll.Models;
using Microsoft.AspNet.Identity;
using MyRepository.Models;

namespace EpamFinalProject.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ApplicationController : Controller
    {
        private readonly IBll _myBll;
        
        public ApplicationController(IBll cont)
        {
            _myBll = cont;
        }
        // GET: Application
        
        public ActionResult Managing()
        {
            var res=_myBll.GetApplicationsForManager();

            return View(res);
        }
        public ActionResult Choose(BaseApplicationManager bam)
        {
            var res = _myBll.GetAppropriateApartments(bam);
            MyBuffer.Bam = bam;
            return View(res);
        }
        public ActionResult ComleteApplication(string ToChoose, string PricePerNight)
        {
            var bam = MyBuffer.Bam;
            MyBuffer.Bam = null;
            _myBll.CompleteApplication(bam,ToChoose,PricePerNight);

            return RedirectToAction("Managing");
        }
        public ActionResult DenyApplication()
        {
            var bam = MyBuffer.Bam;
            MyBuffer.Bam = null;
            _myBll.DenyApplication(bam);

            return RedirectToAction("Managing");
        }
    }
}