using Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<SliderIntro> SliderIntros { get; set; }
        public List<About> Abouts { get; set; }
        public List<Service> Services { get; set; }
        public List<Preference> Preferences { get; set; }
        public PreferenceImg PreferenceImgs { get; set; }
        public List<Doctor> Doctors { get; set; }
        public AboutImg AboutImgs { get; set; }
        public List<Counter> Counters { get; set; }
        public List<Blog> Blogs { get; set; }
        public Consultation Consultations { get; set; }
    }
}
