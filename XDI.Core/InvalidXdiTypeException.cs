using System;
using System.Runtime.Serialization;

namespace XDI.Code
{
    [Serializable]
    internal class InvalidXdiTypeException : Exception
    {
        public InvalidXdiTypeException()
        {
        }

        public InvalidXdiTypeException(string message) : base(message)
        {
        }

        public InvalidXdiTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidXdiTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}