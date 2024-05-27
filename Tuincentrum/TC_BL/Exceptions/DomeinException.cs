using System.Runtime.Serialization;

namespace TC_BL.Exceptions
{
    [Serializable]
    internal class DomeinException : Exception
    {
        public DomeinException()
        {
        }

        public DomeinException(string? message) : base(message)
        {
        }

        public DomeinException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DomeinException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}