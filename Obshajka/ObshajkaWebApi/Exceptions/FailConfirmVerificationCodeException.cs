using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObshajkaWebApi.Exceptions
{

    [Serializable]
    public class FailConfirmVerificationCodeException : Exception
    {
        public FailConfirmVerificationCodeException() { }
        public FailConfirmVerificationCodeException(string message) : base(message) { }
        public FailConfirmVerificationCodeException(string message, Exception inner) : base(message, inner) { }
        protected FailConfirmVerificationCodeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
