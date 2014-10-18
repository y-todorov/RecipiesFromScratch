using System.Threading.Tasks;
using Quartz;
using RecipiesModelNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace RecipiesWebFormApp.Quartz.Jobs
{
    public class RefreshWebsiteJob : JobBase
    {
        public override void Execute(IJobExecutionContext context)
        {
            WebClient client = new WebClient();
            try
            {
                List<Task> tasks = new List<Task>();
                client = new WebClient();
                tasks.Add(client.DownloadStringTaskAsync(new Uri("http://bluesystems.azurewebsites.net/")));
                client = new WebClient();
                tasks.Add(client.DownloadStringTaskAsync(new Uri("http://bluesystems2.azurewebsites.net/")));
                client = new WebClient();
                tasks.Add(client.DownloadStringTaskAsync(new Uri("http://recipiesfromscratchdatabase.apphb.com")));

                //Task.WaitAll(tasks.ToArray());
            }
            catch (Exception)
            {
                // no need to rethrow here
            }
            base.Execute(context);
        }
    }
}