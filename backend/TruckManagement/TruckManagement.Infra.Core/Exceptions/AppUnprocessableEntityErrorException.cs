using System;
using System.Runtime.Serialization;

namespace TruckManagement.Infra.Core.Exceptions
{
    [Serializable]
    public class AppUnprocessableEntityErrorException : AppITException
    {
        public AppUnprocessableEntityErrorException(ExceptionType exceptionType) : base(exceptionType)
        {
        }

        public AppUnprocessableEntityErrorException(string message) : base(message)
        {
        }

        public AppUnprocessableEntityErrorException(Exception exception) : base(exception)
        {
        }

        public AppUnprocessableEntityErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected AppUnprocessableEntityErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}