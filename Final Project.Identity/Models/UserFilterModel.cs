using System;
using System.Collections.Generic;
using System.Text;

namespace Final_Project.Identity.Models
{
    public class UserFilterModel
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        //public int pageIndex { get; set; }
        //public int pageSize { get; set; }
        //public Search search { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string sortField { get; set; }
        public string sortOrder { get; set; }
    }
}
