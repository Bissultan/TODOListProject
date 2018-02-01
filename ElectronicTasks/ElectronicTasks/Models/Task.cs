using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicTasks.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskText { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime TermDate { get; set; }
        public bool IsDone { get; set; }

        public int? UserId { get; set; }
    }
}