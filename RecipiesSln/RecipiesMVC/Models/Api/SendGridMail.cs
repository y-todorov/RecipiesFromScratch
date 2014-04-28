using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecipiesMVC.Models.Api
{
    public class SendGridMailViewModel
    {
        public string Email { get; set; }

        public string Sg_event_id { get; set; }

        public string Sg_message_id { get; set; }

        public long Timestamp { get; set; }

        public string Smtp_id { get; set; }

        public string Event { get; set; }

        public string Id { get; set; }

        public string Uid { get; set; }

        public string Useragent { get; set; }

        public string Url { get; set; }

        public string Reason { get; set; }

        public string Type { get; set; }



    }
}