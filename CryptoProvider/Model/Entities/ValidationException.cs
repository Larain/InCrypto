using System;
using System.Runtime.Serialization;
using icModel.Abstract;

namespace icModel.Model.Entities
{
    public class ValidationException : Exception
    {

        public ICryptoKey Key { get; set; }

        public ValidationException() { }

        public ValidationException(string plainText, ICryptoKey key) : base(plainText)
        {
            Key = key;
        }

        public ValidationException(string message)
            : base(message) { }

        public ValidationException(string message, Exception inner)
            : base(message, inner) { }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}