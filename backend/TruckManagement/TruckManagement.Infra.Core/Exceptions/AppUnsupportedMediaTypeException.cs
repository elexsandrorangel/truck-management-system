using System;
using System.Runtime.Serialization;

namespace TruckManagement.Infra.Core.Exceptions
{
    [Serializable]
    public class AppUnsupportedMediaTypeException : AppITException
    {
        public AppUnsupportedMediaTypeException() : base("Unsupported Media Type")
        {
        }

        public AppUnsupportedMediaTypeException(ExceptionType exceptionType)
            : base(exceptionType)
        {
        }

        public AppUnsupportedMediaTypeException(string message) : base(message)
        {
        }

        public AppUnsupportedMediaTypeException(Exception exception) : base(exception)
        {
        }

        public AppUnsupportedMediaTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected AppUnsupportedMediaTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}