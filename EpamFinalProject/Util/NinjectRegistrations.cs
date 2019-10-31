using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBll.Models;
using Ninject.Modules;


namespace EpamFinalProject.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IBll>().To<MyBllContext>();
        }
    }
}