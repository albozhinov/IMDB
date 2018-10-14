using System;
using System.Runtime.Serialization;

namespace IMDB.Services.Exceptions
{
    [Serializable]
    public class NotEnoughPermissionException : Exception
    {
        public NotEnoughPermissionException()
        {
        }

        public NotEnoughPermissionException(string message) : base(message)
        {
        }

        public NotEnoughPermissionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotEnoughPermissionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}