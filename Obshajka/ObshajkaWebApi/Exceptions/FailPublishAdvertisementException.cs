using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObshajkaWebApi.Exceptions
{

    [Serializable]
    public class FailPublishAdvertisementException : Exception
    {
        public FailPublishAdvertisementException() { }
        public FailPublishAdvertisementException(string message) : base(message) { }
        public FailPublishAdvertisementException(string message, Exception inner) : base(message, inner) { }
        protected FailPublishAdvertisementException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
