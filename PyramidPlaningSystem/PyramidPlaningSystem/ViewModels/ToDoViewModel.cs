using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PyramidPlaningSystem.Models;

namespace PyramidPlaningSystem.ViewModels
{
    public class ToDoModel
    {
        public ToDoViewModel ParentToDo { get; set; }

        public List<ToDoViewModel> ChildToDos { get; set; }

    }

    public class ToDoViewModel
    {
        public ToDo ToDo { get; set; }

        public string UniqueId { get; set; }

        public List<string> ContactIdList { get; set; }
    }
}
