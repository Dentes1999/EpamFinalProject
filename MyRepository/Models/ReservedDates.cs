using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyRepository.Models
{
    public class ReservedDates
    {
        [Key]
        [ForeignKey("Application")]
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
        
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
