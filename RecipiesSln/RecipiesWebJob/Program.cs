using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RecipiesWebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            Timer timer = new Timer(TimeSpan.FromMinutes(10).TotalMilliseconds);
            timer.Start();
            timer.Elapsed += timer_Elapsed;

            Console.ReadLine();
        }

        static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WebClient client = new WebClient();
            try
            {
                Console.WriteLine("Refreshing every minute!");
                Console.WriteLine("http://bluesystems.azurewebsites.net/");
                string res = client.DownloadStringTaskAsync(new Uri("http://bluesystems.azurewebsites.net/")).Result;
                Console.WriteLine(res);

                Console.WriteLine("--------------------------------------------------------------------");

                Console.WriteLine("http://bluesystems2.azurewebsites.net/");
                res = client.DownloadStringTaskAsync(new Uri("http://bluesystems2.azurewebsites.net/")).Result;
                Console.WriteLine(res);
                Console.WriteLine("--------------------------------------------------------------------");

                Console.WriteLine("http://recipiesservices.apphb.com/");
                res = client.DownloadStringTaskAsync(new Uri("http://recipiesservices.apphb.com/")).Result;
                Console.WriteLine(res);
                Console.WriteLine("--------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
