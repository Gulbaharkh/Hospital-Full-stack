using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string AddressDetail { get; set; }
        public string MailAddress { get; set; }
        public string Website { get; set; }
    }
}
