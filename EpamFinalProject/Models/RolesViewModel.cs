using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpamFinalProject.Models
{
    public class RolesViewModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsManager { get; set; }
        public string[] ForCheckManager { get; set; }
        public string[] ForCheckAdmin { get; set; }
    }
}