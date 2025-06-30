using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy_oops.entity
{
    public class Project
    {
        public int projectid { get; set; }
        public string projectname { get; set; }
        public string description { get; set; }
        public DateTime startdate { get; set; }
        public string status { get; set; }

       // public project() { }

        public Project(int id, string name, string desc, DateTime startDate, string _status)
        {
            projectid = id;
            projectname = name;
            description = desc;
            startdate = startDate;
            status = _status;
        }
    }
}
