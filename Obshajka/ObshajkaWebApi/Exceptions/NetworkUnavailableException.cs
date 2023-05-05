using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObshajkaWebApi.Exceptions
{

	[Serializable]
	public class NetworkUnavailableException : Exception
	{
		public NetworkUnavailableException() { }
		public NetworkUnavailableException(string message) : base(message) { }
		public NetworkUnavailableException(string message, Exception inner) : base(message, inner) { }
		protected NetworkUnavailableException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
