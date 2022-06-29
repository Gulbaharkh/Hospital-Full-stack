using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Final_Project.Identity.Models
{
    public class EditModelData
    {
        public string id { get; set; }

        public string Id { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string email { get; set; }

        public bool isAdmin { get; set; }
        /*        public string book { get; set; }
                public int bookId { get; set; }*/
    }
}
