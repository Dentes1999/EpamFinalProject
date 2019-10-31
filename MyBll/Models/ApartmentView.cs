using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBll.Models
{
    public class ApartmentView
    {
        public int ApartmentId { get; set; }
        public string Href { get; set; }
        public string[] Description { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int AmountOfPeople { get; set; }
        public int PricePerNight { get; set; }
        public bool NotAppliable { get; set; }
    }
}
