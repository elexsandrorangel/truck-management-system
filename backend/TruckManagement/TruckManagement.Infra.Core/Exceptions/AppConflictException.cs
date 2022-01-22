using System;
using System.Runtime.Serialization;

namespace TruckManagement.Infra.Core.Exceptions
{
    [Serializable]
    public class AppConflictException : AppITException
    {
        public AppConflictException()
                  : base("Record already exists at the server")
        {
            ExceptionType = ExceptionType.Error;
        }

        public AppConflictException(ExceptionType exceptionType)
            : base(exceptionType)
        {
        }

        public AppConflictException(string message) : base(message)
        {
        }

        public AppConflictException(Exception exception) : base(exception)
        {
        }

        public AppConflictException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected AppConflictException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
