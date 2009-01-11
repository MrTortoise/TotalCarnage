using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom.Exceptions
{
	public class ArgumentEmptyStringException : ArgumentException
	{
		public ArgumentEmptyStringException()
		{ }

		public ArgumentEmptyStringException(string message)
			: base(message)
		{ }

		public ArgumentEmptyStringException(string message, Exception inner)
			: base(message, inner)
		{ }
	}
}
