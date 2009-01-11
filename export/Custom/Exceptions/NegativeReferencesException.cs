using System;


namespace Custom.Exceptions
{
    public class NegativeReferencesException : Exception 
	{
        public NegativeReferencesException()
        {         }

        public NegativeReferencesException(string message)
            :base(message)
        {}

		public NegativeReferencesException(string message, Exception inner)
            : base(message, inner)
        { }


    }
}
