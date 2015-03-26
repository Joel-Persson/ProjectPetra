using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyramidPlaningSystem.Models
{
    public class ToDo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ToDoId { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan? Effort { get; set; }
        public DateTime? Created { get; set; }
        public Guid? ParentId { get; set; }


    }
}
