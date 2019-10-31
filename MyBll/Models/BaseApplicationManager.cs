using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBll.Models
{
    public class BaseApplicationManager
    {
        public string BaseApplicationId { set; get; }
        public string Class { get; set; }
        public string NumOfPeople { get; set; }

        public string UserId { get; set; }
        public string Status { get; set; } = "Waiting for respond";
        public string DateIn { get; set; }
        public string DateOut { get; set; }
    }
}
