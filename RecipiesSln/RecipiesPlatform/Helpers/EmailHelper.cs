using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Mvc;
using RestSharp;
using System.IO;

namespace Helpers
{
    public static class EmailHelper
    {
        public static ContentResult SendComplexMessage(string from, string fromDisplayName, string to, string toDisplayName, string cc, string bcc, string subject,
            string textBody, string htmlBody, byte[] attachmentBytes, string attachmentNameWithExtension)
        {
            //RestClient client = new RestClient();
            //client.BaseUrl = "https://api.mailgun.net/v2";
            //client.Authenticator = new HttpBasicAuthenticator("api", "key-7md8hh5f7cxi062n3x23x7h6nof5fue9");

            //IRestRequest request = new RestRequest();
            //request.AddParameter("domain", "app20716.mailgun.org", ParameterType.UrlSegment);

            //request.Resource = "{domain}/messages";

            //request.AddParameter("from", from);
            //request.AddParameter("to", to);
            //request.AddParameter("cc", cc);
            //request.AddParameter("bcc", bcc);
            //request.AddParameter("subject", subject);
            //request.AddParameter("text", textBody);
            //request.AddParameter("html", htmlBody);

            //request.AddFile("attachment", attachmentBytes, attachmentNameWithExtension);
            //request.Method = Method.POST;
            //RestResponse response = client.Execute(request) as RestResponse;

            //return response;

            try
            {
                MailMessage mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress(to, toDisplayName));

                // From
                mailMsg.From = new MailAddress(from, fromDisplayName);

                if (cc != null)
                {
                    mailMsg.CC.Add(new MailAddress(cc));
                }
                if (bcc != null)
                {
                    mailMsg.Bcc.Add(new MailAddress(bcc));
                }

                // Subject and multipart/alternative Body
                mailMsg.Subject = subject;
                string text = textBody;
                string html = htmlBody;
                if (!string.IsNullOrEmpty(text))
                {
                    mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null,
                        MediaTypeNames.Text.Plain));
                }
                if (!string.IsNullOrEmpty(html))
                {
                    mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null,
                        MediaTypeNames.Text.Html));
                }

                //string file = @"C:\Users\Yordan\Downloads\PurchaseOrder (1).pdf";
                string file = @"G:\Downloads\SQLServer2014-x64-ENU\1033_ENU_LP\x64\redist\Upgrade Advisor\SqlUA.msi";
                Attachment data = null;
                //using (MemoryStream ms = new MemoryStream(attachmentBytes)) // Cannot access a closed stream error !!!
                {
                    MemoryStream ms = new MemoryStream(attachmentBytes);
                    data = new Attachment(ms, attachmentNameWithExtension, null);
                }

                //Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);

                mailMsg.Attachments.Add(data);
                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("azure_3d288ebc5d74d5eacfe19cefb29d9f39@azure.com", "6eM2axnGDV1AdqA");
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
            }
            catch (Exception ex)
            {
                ContentResult res = new ContentResult();
                res.Content = ex.Message + ex.StackTrace;
                return res;

                //Console.WriteLine(ex.Message);
            }

            return null;

        }
    }
}