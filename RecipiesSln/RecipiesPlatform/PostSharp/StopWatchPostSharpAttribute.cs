using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace RecipiesPlatform.PostSharp
{
    [Serializable]
    [StopWatchPostSharpAttribute(AttributeExclude = true)]
    public class StopWatchPostSharpAttribute : OnMethodBoundaryAspect
    {
        private string methodName;

        /// <summary> 
        /// Method executed at build time. Initializes the aspect instance. After the execution 
        /// of <see cref="CompileTimeInitialize"/>, the aspect is serialized as a managed  
        /// resource inside the transformed assembly, and deserialized at runtime. 
        /// </summary> 
        /// <param name="method">Method to which the current aspect instance  
        /// has been applied.</param> 
        /// <param name="aspectInfo">Unused.</param> 
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            this.methodName = method.DeclaringType.FullName + "." + method.Name;
        } 
        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = Stopwatch.StartNew();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            Stopwatch sw = (Stopwatch)args.MethodExecutionTag;
            sw.Stop();

            string output = string.Format("{0} executed in {1} milliseconds!" ,methodName, sw.ElapsedMilliseconds);

            Console.WriteLine(output);
            Trace.WriteLine(output);
        }
    }
}
