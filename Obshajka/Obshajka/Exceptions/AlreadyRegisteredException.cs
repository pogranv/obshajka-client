using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obshajka.Exceptions
{

    [Serializable]
    public class AlreadyRegisteredException : Exception
    {
        public AlreadyRegisteredException() { }
        public AlreadyRegisteredException(string message) : base(message) { }
        public AlreadyRegisteredException(string message, Exception inner) : base(message, inner) { }
        protected AlreadyRegisteredException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
