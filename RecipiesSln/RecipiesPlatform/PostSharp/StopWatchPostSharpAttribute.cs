using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace RecipiesPlatform.PostSharp
{
    [Serializable]
    [StopWatchPostSharpAttribute(AttributeExclude = true)]
    public class StopWatchPostSharpAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = Stopwatch.StartNew();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Stopwatch sw = (Stopwatch)args.MethodExecutionTag;
            sw.Stop();

            string output = string.Format("{0}.{1} executed in {2} milliseconds!", args.Method.DeclaringType.Name,
                args.Method.Name, sw.ElapsedMilliseconds);

            Console.WriteLine(output);
            Trace.WriteLine(output);
        }
    }
}
