using System;
using System.Runtime.Serialization;

namespace TruckManagement.Infra.Core.Exceptions
{
    [Serializable]
    public class AppPreconditionFailedException : AppITException
    {
        public AppPreconditionFailedException(ExceptionType exceptionType) : base(exceptionType)
        {
        }

        public AppPreconditionFailedException(string message) : base(message)
        {
        }

        public AppPreconditionFailedException(Exception exception) : base(exception)
        {
        }

        public AppPreconditionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected AppPreconditionFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}