using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecipiesMVC.Models
{
    public class SendGridMailViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public string Guid { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Key]
        public int PrimaryKey { get; set; }

        public string Email { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Sg_event_id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Sg_message_id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public long? Timestamp { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Smtp_id { get; set; }

        public string Event { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Uid { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Useragent { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Url { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Reason { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Type { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [HiddenInput(DisplayValue = false)] // the user is always NULL because WEB API handles everything and the user is null in that case
        public string ModifiedByUser { get; set; }
        
    }
}