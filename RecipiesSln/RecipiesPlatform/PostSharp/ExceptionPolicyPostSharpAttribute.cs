using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;
using RecipiesWebFormApp.Shared;

namespace RecipiesPlatform.PostSharp
{
    [Serializable]
    public class ExceptionPolicyPostSharpAttribute : OnExceptionAspect
    {
        private Type type;

        public ExceptionPolicyPostSharpAttribute()
            : this(typeof (Exception))
        {
        }

        public ExceptionPolicyPostSharpAttribute(Type type)
            : base()
        {
            this.type = type;
        }

        // Method invoked at build time. 
        // Should return the type of exceptions to be handled.  
        public override Type GetExceptionType(MethodBase method)
        {
            return this.type;
        }
        
        public override void OnException(MethodExecutionArgs args)
        {
            string parameterValues = "";
            
            foreach (object arg in args.Arguments)
            {
                if (parameterValues.Length > 0)
                {
                    parameterValues += ", ";
                }

                if (arg != null)
                {
                    parameterValues += arg.ToString();
                }
                else
                {
                    parameterValues += "null";
                }
            }
            string exDescription = string.Format("{6:yyyy/MM/dd hh:mm:ss fff} Exception {0} in {1}.{2} invoked with arguments {3}. Message: {4} Stacktrace: {5} \n", args.Exception.GetType().Name,
                args.Method.DeclaringType.FullName, args.Method.Name, parameterValues, args.Exception.Message, args.Exception.StackTrace, DateTime.Now);

            LogentriesHelper.WriteMessage(exDescription, LogentriesMessageType.Exception);
        }
    } 
}
