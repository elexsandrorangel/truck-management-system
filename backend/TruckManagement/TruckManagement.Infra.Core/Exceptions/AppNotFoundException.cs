using System;
using System.Runtime.Serialization;

namespace TruckManagement.Infra.Core.Exceptions
{
    [Serializable]
    public class AppNotFoundException : AppITException
    {
        public AppNotFoundException()
            : base("Ops! Resource not found.")
        {
        }

        public AppNotFoundException(ExceptionType exceptionType)
            : base(exceptionType)
        {
        }

        public AppNotFoundException(string message)
            : base(message)
        {
        }

        public AppNotFoundException(Exception exception) : base(exception)
        {
        }

        public AppNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected AppNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
