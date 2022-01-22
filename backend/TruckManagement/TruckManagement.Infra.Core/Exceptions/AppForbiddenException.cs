using System;
using System.Runtime.Serialization;

namespace TruckManagement.Infra.Core.Exceptions
{
    [Serializable]
    public class AppForbiddenException : AppITException
    {
        public AppForbiddenException()
            : base("Sorry! This resource have restrict access level!")
        {
        }

        public AppForbiddenException(ExceptionType exceptionType)
            : base(exceptionType)
        {
        }

        public AppForbiddenException(string message)
            : base(message)
        {
        }

        public AppForbiddenException(Exception exception)
            : base(exception)
        {
        }

        public AppForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected AppForbiddenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}