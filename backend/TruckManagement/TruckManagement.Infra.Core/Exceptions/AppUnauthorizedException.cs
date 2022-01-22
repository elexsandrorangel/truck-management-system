using System;
using System.Runtime.Serialization;

namespace TruckManagement.Infra.Core.Exceptions
{
    [Serializable]
    public class AppUnauthorizedException : AppITException
    {
        public AppUnauthorizedException(ExceptionType exceptionType) : base(exceptionType)
        {
        }

        public AppUnauthorizedException(string message) : base(message)
        {
        }

        public AppUnauthorizedException(Exception exception) : base(exception)
        {
        }

        public AppUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected AppUnauthorizedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}