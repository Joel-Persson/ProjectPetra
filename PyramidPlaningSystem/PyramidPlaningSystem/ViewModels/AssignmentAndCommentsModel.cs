using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PyramidPlaningSystem.Models;

namespace PyramidPlaningSystem.ViewModels
{
    public class AssignmentAndCommentsModel
    {
        public Assignment Assignment { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}