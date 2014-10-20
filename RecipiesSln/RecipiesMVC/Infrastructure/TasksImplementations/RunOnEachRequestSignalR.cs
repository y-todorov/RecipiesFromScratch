using System.Threading;
using RecipiesMVC.Infrastructure.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace RecipiesMVC.Infrastructure.TasksImplementations
{
    public class RunOnEachRequestSignalR : IRunOnEachRequest
    {
        public void Execute()
        {
            if (HttpContext.Current != null)
            {
                var request = HttpContext.Current.Request;
                var sw = Stopwatch.StartNew();

                string key = request.RawUrl + Thread.CurrentThread.ManagedThreadId; 
                CacheItem ci = new CacheItem(key, sw);
                CacheItemPolicy cip = new CacheItemPolicy() {AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1)};
                MemoryCache.Default.Add(ci, cip);
            }
        }
    }
}