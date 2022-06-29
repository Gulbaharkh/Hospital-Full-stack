using Final_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderIntro> SliderIntros { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<AboutImg> AboutImgs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<PreferenceImg> PreferenceImgs { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
    }
}
