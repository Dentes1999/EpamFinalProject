using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBll.Models;
using Microsoft.AspNet.Identity;
using MyRepository.Models;

namespace EpamFinalProject.Models
{
    public static class MyBuffer
    {
         public static BaseApplicationManager Bam { get; set; }
    }
}