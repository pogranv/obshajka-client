using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obshajka.Exceptions
{

	[Serializable]
	public class IncorrectLoginOrPasswordException : Exception
	{
		public IncorrectLoginOrPasswordException() { }
		public IncorrectLoginOrPasswordException(string message) : base(message) { }
		public IncorrectLoginOrPasswordException(string message, Exception inner) : base(message, inner) { }
		protected IncorrectLoginOrPasswordException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
