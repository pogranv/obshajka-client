using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObshajkaWebApi.Exceptions
{

	[Serializable]
	public class FailRegisterUserException : Exception
	{
		public FailRegisterUserException() { }
		public FailRegisterUserException(string message) : base(message) { }
		public FailRegisterUserException(string message, Exception inner) : base(message, inner) { }
		protected FailRegisterUserException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
