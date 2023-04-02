using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obshajka.Exceptions
{

    [Serializable]
    public class SendVerificationCodeException : Exception
    {
        public SendVerificationCodeException() { }
        public SendVerificationCodeException(string message) : base(message) { }
        public SendVerificationCodeException(string message, Exception inner) : base(message, inner) { }
        protected SendVerificationCodeException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
