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
using RecipiesMVC.Models.Api;

namespace RecipiesMVC.Controllers.WebApis
{
    [AllowAnonymous]
    public class SendGridMailEventController : ApiController
    {
        // http://stackoverflow.com/questions/14588397/disable-windows-authentication-for-webapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        public void ProcessEmailEvent2([FromBody]string value)
        {
            var currRequest = HttpContext.Current.Request;
        }

        [HttpPost]
        public void ProcessEvent()
        {
            var currentRequest = HttpContext.Current.Request;
            currentRequest.InputStream.Position = 0;
            using (StreamReader reader = new StreamReader(currentRequest.InputStream))
            {
                string jsonArrayAsString = reader.ReadToEnd();

                var result = JsonConvert.DeserializeObject<List<SendGridMailViewModel>>(jsonArrayAsString);

                JArray a = JArray.Parse(jsonArrayAsString);

                foreach (JObject o in a.Children<JObject>())
                {
                    foreach (JProperty p in o.Properties())
                    {
                        string name = p.Name;
                        string value = p.Value.ToString();
                    }
                }
            }
            

        }
    }
}
