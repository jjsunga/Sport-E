using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sport_E.Controllers
{
    public class NotificationBase
    {
        public NotificationBase()
        {
            ChangeDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "To")]
        [StringLength(100)]
        public string ToEmail { get; set; }

        [Display(Name = "Change Made By")]
        [StringLength(100)]
        public string ChangedByEmail { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Display(Name = "Notification Time")]
        public DateTime ChangeDate { get; set; }

        public bool Read { get; set; }
    }
}