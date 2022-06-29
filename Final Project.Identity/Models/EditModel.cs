using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Final_Project.Identity.Models
{
    public class EditModel
    {
        public string Id { get; set; }
        public string Fullname { get; set; }
       
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
