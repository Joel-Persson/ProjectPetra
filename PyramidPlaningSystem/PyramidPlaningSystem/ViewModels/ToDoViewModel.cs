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
        public ToDo ParentToDo { get; set; }

        public List<ToDo> ChildToDos { get; set; }
    }
}
