using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace RecipiesPlatform.PostSharp
{
    /// <summary> 
    /// Aspect that, when applied on a method, catches all its exceptions, 
    /// assign them a GUID, log them, and replace them by an <see cref="InternalException"/>. 
    /// </summary> 
    [Serializable]
    public class ExceptionPolicyPostSharpAttribute : OnExceptionAspect
    {
        /// <summary> 
        /// Method invoked upon failure of the method to which the current 
        /// aspect is applied. 
        /// </summary> 
        /// <param name="args">Information about the method being executed.</param> 
        public override void OnException(MethodExecutionArgs args)
        {
            Guid guid = Guid.NewGuid();

            Trace.TraceError("Exception {0} handled by ExceptionPolicyAttribute: {1}",
                guid, args.Exception.ToString());

            throw new ApplicationException(
                string.Format("An internal exception has occurred. Use the id {0} " +
                "for further reference to this issue.", guid));
        }
    } 
}
