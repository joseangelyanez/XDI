using System;
using System.Runtime.Serialization;

namespace XDI.Core.Outputs
{
    [Serializable]
    internal class CodeOutputException : Exception
    {
        public CodeOutputException()
        {
        }

        public CodeOutputException(string message) : base(message)
        {
        }

        public CodeOutputException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CodeOutputException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}