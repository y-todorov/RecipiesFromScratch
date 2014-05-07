using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RecipiesModelNS;
using RecipiesMVC.Helpers;
using RecipiesMVC.Models;

namespace RecipiesMVC.Controllers.WebApis
{
    [AllowAnonymous]
    public class SendGridMailEventController : ApiController
    {
        // http://stackoverflow.com/questions/14588397/disable-windows-authentication-for-webapi

        [HttpGet]
        public string ProcessEmailEvent2([FromBody]string value)
        {
            var currRequest = HttpContext.Current.Request;
            return "Everything is OK";
        }

        [HttpPost]
        public void ProcessEvent()
        {
            var currentRequest = HttpContext.Current.Request;
            currentRequest.InputStream.Position = 0;
            using (StreamReader reader = new StreamReader(currentRequest.InputStream))
            {
                string jsonArrayAsString = reader.ReadToEnd();

                List<SendGridMailViewModel> listOfModels = JsonConvert.DeserializeObject<List<SendGridMailViewModel>>(jsonArrayAsString);

                foreach (SendGridMailViewModel sendGridMailViewModel in listOfModels)
                {
                    SendGridMail sgm = null;
                    // save only the last open

                    //When using LINQ to Entities, it will automatically convert it to LINQ to SQL. And if the database field you are doing a .Equals on does not have a collate of
                    //NOCASE (SQLite in my example) then it will always be case-sensitive. In otherwords, the database defines how to do the string comparison rather than code.
                    // this is the error if you use Enum.GetName(typeof(SendGridEmailEvent), SendGridEmailEvent.Open), StringComparison.InvariantCultureIgnoreCase));
                    string openEventName = Enum.GetName(typeof(SendGridEmailEvent), SendGridEmailEvent.open);

                    if (string.Equals(openEventName, sendGridMailViewModel.Event, StringComparison.InvariantCultureIgnoreCase))
                    {
                        sgm =
                            ContextFactory.Current.SendGridMails.FirstOrDefault(
                                s => s.Guid == sendGridMailViewModel.Guid && string.Equals(s.Event, openEventName));
                        if (sgm != null)
                        {
                            sgm.ModifiedDate = DateTime.Now;
                            // no need for this actually, this will be set in YordanBaseEntity, but only if the entity is modified, so we HAVE TO 'touch it'
                        }
                        else
                        {
                            sgm = new SendGridMail()
                            {
                                Email = sendGridMailViewModel.Email,
                                Event = sendGridMailViewModel.Event,
                                Id = sendGridMailViewModel.Id,
                                PrimaryKey = sendGridMailViewModel.PrimaryKey,
                                Reason = sendGridMailViewModel.Reason,
                                Sg_event_id = sendGridMailViewModel.Sg_event_id,
                                Sg_message_id = sendGridMailViewModel.Sg_message_id,
                                Smtp_id = sendGridMailViewModel.Smtp_id,
                                Timestamp = sendGridMailViewModel.Timestamp,
                                Type = sendGridMailViewModel.Type,
                                Uid = sendGridMailViewModel.Uid,
                                Url = sendGridMailViewModel.Url,
                                UserAgent = sendGridMailViewModel.Useragent,
                                Guid = sendGridMailViewModel.Guid,
                                ModifiedByUser = sendGridMailViewModel.ModifiedByUser,
                                // move this out !!! in some helper method
                                ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"))//sendGridMailViewModel.ModifiedDate
                            };
                            ContextFactory.Current.SendGridMails.Add(sgm);
                        }
                    }
                    else
                    {
                        sgm = new SendGridMail()
                        {
                            Email = sendGridMailViewModel.Email,
                            Event = sendGridMailViewModel.Event,
                            Id = sendGridMailViewModel.Id,
                            PrimaryKey = sendGridMailViewModel.PrimaryKey,
                            Reason = sendGridMailViewModel.Reason,
                            Sg_event_id = sendGridMailViewModel.Sg_event_id,
                            Sg_message_id = sendGridMailViewModel.Sg_message_id,
                            Smtp_id = sendGridMailViewModel.Smtp_id,
                            Timestamp = sendGridMailViewModel.Timestamp,
                            Type = sendGridMailViewModel.Type,
                            Uid = sendGridMailViewModel.Uid,
                            Url = sendGridMailViewModel.Url,
                            UserAgent = sendGridMailViewModel.Useragent,
                            Guid = sendGridMailViewModel.Guid,
                            ModifiedByUser = sendGridMailViewModel.ModifiedByUser,
                            // move this out !!! in some helper method
                            ModifiedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow,
                    TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time")) // sendGridMailViewModel.ModifiedDate
                        };
                        ContextFactory.Current.SendGridMails.Add(sgm);
                    }

                }
                ContextFactory.Current.SaveChanges();
                ControllerHelper.RemoveAllDataItemsFromCache();
            }
        }

        enum SendGridEmailEvent
        {
            processed,
            dropped,
            deferred,
            bounce,
            open,
            click,
            spamReport,
            unsubscribe,

        }
    }
}
