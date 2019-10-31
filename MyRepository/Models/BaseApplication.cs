using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyRepository.Models
{
    public class BaseApplication
    {
        [Key]
        public int BaseApplicationId { set; get; }
        public string Class { get; set; }
        public int NumOfPeople { get; set; }
        
        public string UserId { get; set; }
        public string Status { get; set; } = "Waiting for respond";
        
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }

    }
}
