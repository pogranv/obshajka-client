using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObshajkaWebApi.Exceptions
{

    [Serializable]
    public class FailGetAdvertisementsException : Exception
    {
        public FailGetAdvertisementsException() { }
        public FailGetAdvertisementsException(string message) : base(message) { }
        public FailGetAdvertisementsException(string message, Exception inner) : base(message, inner) { }
        protected FailGetAdvertisementsException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
