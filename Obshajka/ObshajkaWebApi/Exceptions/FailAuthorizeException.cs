using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObshajkaWebApi.Exceptions
{
	[Serializable]
	public class FailAuthorizeException : Exception
	{
		public FailAuthorizeException() { }
		public FailAuthorizeException(string message) : base(message) { }
		public FailAuthorizeException(string message, Exception inner) : base(message, inner) { }
		protected FailAuthorizeException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
