using System;
using System.Runtime.Serialization;

namespace TruckManagement.Infra.Core.Exceptions
{
    [Serializable]
    public class AppTimeoutException : AppITException
    {
        public AppTimeoutException(string message) : base(message)
        {
        }

        public AppTimeoutException(Exception exception) : base(exception)
        {
        }

        public AppTimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected AppTimeoutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}