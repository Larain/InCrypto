using System;
using System.Runtime.Serialization;
using icModel.Abstract;

namespace icModel.Model.Entities {
    public class CipherException : Exception {

        public ICryptoKey Key { get; set; }

        public CipherException() {}

        public CipherException(string plainText, ICryptoKey key) : base(plainText) {
            Key = key;
        }

        public CipherException(string message)
            : base(message) {}

        public CipherException(string message, Exception inner)
            : base(message, inner) {}

        protected CipherException(SerializationInfo info, StreamingContext context)
            : base(info, context) {}
    }
}