using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Web;
using Quartz;
using RecipiesPlatform.PostSharp;
using System.Threading.Tasks;

namespace RecipiesWebFormApp.Shared
{
    public enum LogentriesMessageType
    {
        Alert,
        Warning,
        Critical,
        Fatal,
        Exception,
        Debug,
        Info
    }

    [StopWatchPostSharp]
    public static class LogentriesHelper
    {
        public static void WriteMessage(string message, LogentriesMessageType logentriesMessageType)
        {
            Task.Factory.StartNew(() =>
            {
                string server = "data.logentries.com";
                Int32 port = 80;
                string charDelimiter = "_";
                string messageTypeMarker = "Info";

                switch (logentriesMessageType)
                {
                    case LogentriesMessageType.Alert:
                        messageTypeMarker = string.Format("{0}{1}{0}", charDelimiter,"Alert");
                        break;
                    case LogentriesMessageType.Warning:
                        messageTypeMarker = string.Format("{0}{1}{0}", charDelimiter, "Warning");
                        break;
                    case LogentriesMessageType.Critical:
                        messageTypeMarker = string.Format("{0}{1}{0}", charDelimiter, "Critical");

                        break;
                    case LogentriesMessageType.Fatal:
                        messageTypeMarker = string.Format("{0}{1}{0}", charDelimiter, "Fatal");
                        break;
                    case LogentriesMessageType.Exception:
                        messageTypeMarker = string.Format("{0}{1}{0}", charDelimiter, "Exception");
                        break;
                    case LogentriesMessageType.Debug:
                        messageTypeMarker = string.Format("{0}{1}{0}", charDelimiter, "Debug");
                        break;
                    case LogentriesMessageType.Info:
                        messageTypeMarker = string.Format("{0}{1}{0}", charDelimiter, "Info");
                        break;
                    default:
                        messageTypeMarker = string.Format("{0}{1}{0}", charDelimiter, "Info");
                        break;
                }

                string token = ConfigurationManager.AppSettings["LOGENTRIES_TOKEN"];
                if (string.IsNullOrEmpty(token))
                {
                    throw new ApplicationException("'LOGENTRIES_TOKEN' cannot be empty in the app.config or web.config");
                }
                // SO FUCKING IMPORTANT TO AND WITH \r\n
                message = string.Format("{0} {1} {2} {3}", token, message, messageTypeMarker, "\r\n");




                using (TcpClient client = new TcpClient(server, port))
                {
                    client.NoDelay = true;

                    // Translate the passed message into UTF8 and store it as a Byte array.
                    Byte[] data = Encoding.UTF8.GetBytes(message);

                    using (NetworkStream stream = client.GetStream())
                    {
                        // Send the message to the connected TcpServer. 
                        stream.WriteAsync(data, 0, data.Length);
                    }
                }
            });
        }
    }
}