using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using RecipiesWebFormApp.Shared;

namespace RecipiesWebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer(TimeSpan.FromMinutes(10).TotalMilliseconds);
            timer.Elapsed += timer_Elapsed;
            timer.Start();

            Console.ReadLine();
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WebClient client = new WebClient();
            try
            {
                Console.WriteLine("Refreshing every 10 minutes!");
                Console.WriteLine("http://bluesystems.azurewebsites.net/");
                LogentriesHelper.WriteMessage("Refreshing http://bluesystems.azurewebsites.net/", LogentriesMessageType.Info);
                string res = client.DownloadStringTaskAsync(new Uri("http://bluesystems.azurewebsites.net/")).Result;
                Console.WriteLine(res);
               
                Console.WriteLine("--------------------------------------------------------------------");

                Console.WriteLine("http://bluesystems2.azurewebsites.net/");
                LogentriesHelper.WriteMessage("Refreshing http://bluesystems2.azurewebsites.net/", LogentriesMessageType.Info);
                res = client.DownloadStringTaskAsync(new Uri("http://bluesystems2.azurewebsites.net/")).Result;
                Console.WriteLine(res);
                Console.WriteLine("--------------------------------------------------------------------");
               

                Console.WriteLine("http://recipiesservices.apphb.com/");
                LogentriesHelper.WriteMessage("Refreshing http://recipiesservices.apphb.com/", LogentriesMessageType.Info);
                res = client.DownloadStringTaskAsync(new Uri("http://recipiesservices.apphb.com/")).Result;
                Console.WriteLine(res);
                Console.WriteLine("--------------------------------------------------------------------");
                
                
            }
            catch (Exception ex)
            {
                LogentriesHelper.WriteMessage(ex.Message, LogentriesMessageType.Info);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
