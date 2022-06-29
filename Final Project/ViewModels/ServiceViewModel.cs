using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.ViewModels
{
    public class ServiceViewModel
    {
        public List<Service> Services { get; set; }
        public List<Preference> Preferences { get; set; }
        public Consultation Consultations { get; set; }
    }
}
