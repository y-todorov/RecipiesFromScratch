using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Web;
using Quartz;
using RecipiesPlatform.PostSharp;
using System.Threading.Tasks;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

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

    public static class LogentriesHelper
    {
        private static readonly X509Certificate LogentriesX509Certificate = new X509Certificate2(Encoding.UTF8.GetBytes("-----BEGIN CERTIFICATE-----\r\nMIIFSjCCBDKgAwIBAgIDBQMSMA0GCSqGSIb3DQEBBQUAMGExCzAJBgNVBAYTAlVT\r\nMRYwFAYDVQQKEw1HZW9UcnVzdCBJbmMuMR0wGwYDVQQLExREb21haW4gVmFsaWRh\r\ndGVkIFNTTDEbMBkGA1UEAxMSR2VvVHJ1c3QgRFYgU1NMIENBMB4XDTEyMDkxMDE5\r\nNTI1N1oXDTE2MDkxMTIxMjgyOFowgcExKTAnBgNVBAUTIEpxd2ViV3RxdzZNblVM\r\nek1pSzNiL21hdktiWjd4bEdjMRMwEQYDVQQLEwpHVDAzOTM4NjcwMTEwLwYDVQQL\r\nEyhTZWUgd3d3Lmdlb3RydXN0LmNvbS9yZXNvdXJjZXMvY3BzIChjKTEyMS8wLQYD\r\nVQQLEyZEb21haW4gQ29udHJvbCBWYWxpZGF0ZWQgLSBRdWlja1NTTChSKTEbMBkG\r\nA1UEAxMSYXBpLmxvZ2VudHJpZXMuY29tMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8A\r\nMIIBCgKCAQEAxcmFqgE2p6+N9lM2GJhe8bNUO0qmcw8oHUVrsneeVA66hj+qKPoJ\r\nAhGKxC0K9JFMyIzgPu6FvuVLahFZwv2wkbjXKZLIOAC4o6tuVb4oOOUBrmpvzGtL\r\nkKVN+sip1U7tlInGjtCfTMWNiwC4G9+GvJ7xORgDpaAZJUmK+4pAfG8j6raWgPGl\r\nJXo2hRtOUwmBBkCPqCZQ1mRETDT6tBuSAoLE1UMlxWvMtXCUzeV78H+2YrIDxn/W\r\nxd+eEvGTSXRb/Q2YQBMqv8QpAlarcda3WMWj8pkS38awyBM47GddwVYBn5ZLEu/P\r\nDiRQGSmLQyFuk5GUdApSyFETPL6p9MfV4wIDAQABo4IBqDCCAaQwHwYDVR0jBBgw\r\nFoAUjPTZkwpHvACgSs5LdW6gtrCyfvwwDgYDVR0PAQH/BAQDAgWgMB0GA1UdJQQW\r\nMBQGCCsGAQUFBwMBBggrBgEFBQcDAjAdBgNVHREEFjAUghJhcGkubG9nZW50cmll\r\ncy5jb20wQQYDVR0fBDowODA2oDSgMoYwaHR0cDovL2d0c3NsZHYtY3JsLmdlb3Ry\r\ndXN0LmNvbS9jcmxzL2d0c3NsZHYuY3JsMB0GA1UdDgQWBBRaMeKDGSFaz8Kvj+To\r\nj7eMOtT/zTAMBgNVHRMBAf8EAjAAMHUGCCsGAQUFBwEBBGkwZzAsBggrBgEFBQcw\r\nAYYgaHR0cDovL2d0c3NsZHYtb2NzcC5nZW90cnVzdC5jb20wNwYIKwYBBQUHMAKG\r\nK2h0dHA6Ly9ndHNzbGR2LWFpYS5nZW90cnVzdC5jb20vZ3Rzc2xkdi5jcnQwTAYD\r\nVR0gBEUwQzBBBgpghkgBhvhFAQc2MDMwMQYIKwYBBQUHAgEWJWh0dHA6Ly93d3cu\r\nZ2VvdHJ1c3QuY29tL3Jlc291cmNlcy9jcHMwDQYJKoZIhvcNAQEFBQADggEBAAo0\r\nrOkIeIDrhDYN8o95+6Y0QhVCbcP2GcoeTWu+ejC6I9gVzPFcwdY6Dj+T8q9I1WeS\r\nVeVMNtwJt26XXGAk1UY9QOklTH3koA99oNY3ARcpqG/QwYcwaLbFrB1/JkCGcK1+\r\nAg3GE3dIzAGfRXq8fC9SrKia+PCdDgNIAFqe+kpa685voTTJ9xXvNh7oDoVM2aip\r\nv1xy+6OfZyGudXhXag82LOfiUgU7hp+RfyUG2KXhIRzhMtDOHpyBjGnVLB0bGYcC\r\n566Nbe7Alh38TT7upl/O5lA29EoSkngtUWhUnzyqYmEMpay8yZIV4R9AuUk2Y4HB\r\nkAuBvDPPm+C0/M4RLYs=\r\n-----END CERTIFICATE-----"));

        public static bool ValidateServerCertificate(
             object sender,
             X509Certificate certificate,
             X509Chain chain,
             SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers. 
            return false;
        }

        public static void WriteMessage(string message, LogentriesMessageType logentriesMessageType)
        {
            Task.Factory.StartNew(() =>
            {
                string server = "data.logentries.com";
                Int32 nonSecurePort80 = 80;
                Int32 sslPort443 = 443;
                string charDelimiter = "_";
                string messageTypeMarker = "Info";

                switch (logentriesMessageType)
                {
                    case LogentriesMessageType.Alert:
                        messageTypeMarker = string.Format("{0}{1}{0}", charDelimiter, "Alert");
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

                string hostName = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostEntry(hostName);

                IPAddress[] addr = ipEntry.AddressList;

                // NO NEED FOR ALL ADDRESSES, TAKE JUST THE LAST ONE
                //StringBuilder sbIpAddress = new StringBuilder();
                //for (int i = 0; i < addr.Length; i++)
                //{
                //    sbIpAddress.AppendFormat("IP Address {0}: {1} ", i, addr[i].ToString());
                //}
                string lastIpAddress = string.Empty;
                if (addr.Length > 0)
                {
                    lastIpAddress = addr.Last().ToString();
                }

                //Trace.WriteLine(message); DO NOT DO THIS HERE!!! THIS IS VERY SLOW. THIS CAUSES THE THREAD TO WRITE TO THR CALLING THREAD!!! SLOW
                // SO FUCKING IMPORTANT TO AND WITH \r\n
                message = message.Replace("\r\n", string.Empty).Replace("\n", string.Empty);
                
                message = string.Format("{0} {1} {2} HostName: {3} IP: {4} {5}", token, message, messageTypeMarker, hostName, lastIpAddress, "\r\n");
             

                using (TcpClient sslclient = new TcpClient(server, sslPort443))
                {
                    sslclient.NoDelay = true;
                    // Translate the passed message into UTF8 and store it as a Byte array.
                    Byte[] data = Encoding.UTF8.GetBytes(message);

                    using (SslStream sslStream = new SslStream(sslclient.GetStream(), false, ValidateServerCertificate, null))
                    {
                        try
                        {
                            sslStream.AuthenticateAsClient(server);
                            sslStream.WriteAsync(data, 0, data.Length);
                        }
                        catch (AuthenticationException e)
                        {
                            using (TcpClient client = new TcpClient(server, nonSecurePort80))
                            {
                                client.NoDelay = true;
                                using (NetworkStream stream = client.GetStream())
                                {
                                    stream.WriteAsync(data, 0, data.Length);
                                }
                            }
                        }

                    }
                }
            });
        }
    }
}