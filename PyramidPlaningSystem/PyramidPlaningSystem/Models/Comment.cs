using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PyramidPlaningSystem.Models
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CommentId { get; set; }
       
        [Required]
        public string Author { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public virtual ToDo ToDo { get; set; }
    }
}