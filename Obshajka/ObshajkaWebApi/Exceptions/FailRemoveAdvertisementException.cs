using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObshajkaWebApi.Exceptions
{

    [Serializable]
    public class FailRemoveAdvertisementException : Exception
    {
        public FailRemoveAdvertisementException() { }
        public FailRemoveAdvertisementException(string message) : base(message) { }
        public FailRemoveAdvertisementException(string message, Exception inner) : base(message, inner) { }
        protected FailRemoveAdvertisementException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
