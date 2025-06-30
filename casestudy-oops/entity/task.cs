using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy_oops.entity
{
    public  class ProjectTask
    {
        public int taskid { get; set; }
        public string taskname { get; set; }
        public int projectid { get; set; }
        public int employeeid { get; set; }
        public string status { get; set; }

      public ProjectTask() { }

        public ProjectTask(int taskId, string name, int projId, int empId, string _status)
        {
            taskid = taskId;
            taskname = name;
            projectid = projId;
            employeeid = empId;
            status = _status;
        }

    }
}
