using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Final_Project.Identity.Models
{
    public class AppUser : IdentityUser
    {
        public string Fullname { get; set; }
        public bool IsDeleted { get; set; }
    }
}
