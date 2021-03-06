﻿using System;
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
        public int? Effort { get; set; }
        public DateTime? Created { get; set; }
        public Guid? ParentId { get; set; }
        public Priority? Priority { get; set; }
        public bool Deleted { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual Projects Project { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }

    public enum Priority
    {
        High = 1,
        Medium,
        Low
    }

}
