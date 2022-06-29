using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models
{
    public class Consultation
    {
        public int Id { get; set; }
        public string Doctor { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
    }
}
