using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PyramidPlaningSystem.Models
{
    public class Assignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        public virtual ToDo Todo { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
        [Required]
        public string AddedBy { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
    }
}