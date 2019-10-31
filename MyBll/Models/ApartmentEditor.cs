using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MyBll.Models
{
    public class ApartmentEditor
    {
        public int ApartmentId { get; set; }
        public string Name { get; set; }
        public int PricePerNight { get; set; }
        public int AmountOfPeople { get; set; }

        public string Href1 { get; set; } = "fake";
        public string Class { get; set; }
        public string Description { get; set; }
        public bool NotAppliable { get; set; }
        public string[] Forcheck { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        public HttpPostedFileBase Href2 { get; set; }
    }
}
