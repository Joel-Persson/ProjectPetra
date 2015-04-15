using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyramidPlaningSystem.Models
{
    public class Projects
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Projectname { get; set; }

        public virtual ICollection<ToDo> ToDos { get; set; }
    }
}
