using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRepository.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { set; get; }
        [ForeignKey("Apartment")]
        public int ApartmentId { get; set; }
        public virtual Apartment Apartment { get; set; }
        public string UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int NumOfPeople { get; set; }
        public int Price { get; set; }
        public string Status { get; set; } = "Waiting for payment";
        public bool ToPay { get; set; } = true;
        public virtual ReservedDates ForDates { get; set; }

    }
}
