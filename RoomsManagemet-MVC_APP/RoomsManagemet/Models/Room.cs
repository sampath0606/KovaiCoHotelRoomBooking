using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OfferManagement.Models
{
    public class Room
    {
        [Required]
        [Display(Name = "RoomNo *")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string RoomNo { get; set; }

        [Display(Name = "RoomType *")]
        public string RoomType { get; set; }

        [Display(Name = "Staus")]
        public string Status { get; set; }

    }

    public class Booking
    {

        public DateTime FromDate { get; set; } = DateTime.Now;

        public DateTime ToDate { get; set; } = DateTime.Now;

        public string RoomType { get; set; } = "Single";

    }
}