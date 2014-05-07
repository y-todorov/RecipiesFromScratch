using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Mvc;
using RestSharp;
using System.IO;
using SendGridMail;

namespace Helpers
{
    public static class EmailHelper
    {
        public static void SendComplexMessage(string from, string fromDisplayName, string to, string toDisplayName, string cc, string bcc, string subject,
            string textBody, string htmlBody, byte[] attachmentBytes, string attachmentNameWithExtension)
        {
            SendGrid mailMsg = SendGrid.GetInstance();

            mailMsg.AddTo(to); 
            
            mailMsg.From = new MailAddress(from, fromDisplayName);

            // https://github.com/sendgrid/sendgrid-php/issues/23
            if (cc != null)
            {
                mailMsg.AddTo(cc);
            }
            if (bcc != null)
            {
                mailMsg.AddBcc(bcc);
            }

            mailMsg.Subject = subject;
            string text = textBody;
            string html = htmlBody;
            if (!string.IsNullOrEmpty(text))
            {
                mailMsg.Text = text;
            }
            if (!string.IsNullOrEmpty(html))
            {
                mailMsg.Html = html;
            }

            MemoryStream ms = new MemoryStream(attachmentBytes);

            mailMsg.AddAttachment(ms, attachmentNameWithExtension);

            Dictionary<string, string> uniqueArgs = new Dictionary<string, string>();
            uniqueArgs.Add("Guid", Guid.NewGuid().ToString());
            mailMsg.AddUniqueArgs(uniqueArgs);

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("azure_3d288ebc5d74d5eacfe19cefb29d9f39@azure.com", "6eM2axnGDV1AdqA");

            // Create an Web transport for sending email.
            Web transportWeb = Web.GetInstance(credentials);

            // Send the email.
            transportWeb.Deliver(mailMsg);
        }
    }
}