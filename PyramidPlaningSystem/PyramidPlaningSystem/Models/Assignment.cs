using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PyramidPlaningSystem.Models
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public virtual ToDo Todo { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string AddedBy { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}