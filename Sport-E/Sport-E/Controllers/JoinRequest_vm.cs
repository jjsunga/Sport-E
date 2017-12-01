using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sport_E.Controllers
{
    public class JoinRequestBase
    {
        public JoinRequestBase()
        {
            Email_j = "";
            Event_j = "";
            PublicationStatus = "";
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Request From")]
        [StringLength(100)]
        public string Email_j { get; set; }

        [Display(Name = "Event Name")]
        [StringLength(100)]
        public string Event_j { get; set; }

        [Display(Name = "Status")]
        public string PublicationStatus { get; set; }

    }
}