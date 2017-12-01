using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sport_E.Controllers
{

    public class CommentAdd
    {
        public CommentAdd()
        {

        }

        [Required]
        public string Email { get; set; }

        [StringLength(200)]
        public string Msg { get; set; }

    }
    public class CommentBase : CommentAdd
    {

        public CommentBase()
        {

        }

        [Key]
        public int CommentId { get; set; }



    }

    public class CommentWithEventInfo : CommentBase
    {
        public EventBase Event { get; set; }
    }

    public class CommentWithEventId : CommentBase
    {
        [Display(Name = "Event Id")]
        public int EventId { get; set; }
    }
}