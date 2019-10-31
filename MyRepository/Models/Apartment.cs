using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyRepository.Models
{
    public class Apartment
    {
        [Key] public int ApartmentId { get; set; }
        public string Name { get; set; }
        public int PricePerNight { get; set; }
        public int AmountOfPeople { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "Please choose file to upload.")]
        public string Href { get; set; }
        public string Class { get; set; }
        public string Description { get; set; }
        public bool NotAppliable { get; set; } = false;
        public virtual ICollection<Application> Applications { get; set; }

        /*public Apartment()
        {
            Applications=new List<Application>();
        }*/

        public List<DateTime> GetReservedDates()
        {
            var dates = new List<DateTime>();
            foreach (var app in Applications.Where(a=>(a.Status=="Waiting for payment" || a.Status == "Purchased")))
            {
                var resdates = app.ForDates;
                var start = resdates.DateStart;
                var end = resdates.DateEnd;
                for (var dt = start; dt <= end; dt = dt.AddDays(1))
                {
                    dates.Add(dt);
                }
            }

            return dates;
        }
    }
}
