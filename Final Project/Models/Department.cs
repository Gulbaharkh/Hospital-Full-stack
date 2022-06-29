using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string DepartmentTitle { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentInfo { get; set; }
        public string FindDoctor { get; set; }
        public string Appointment { get; set; }
    }
}
