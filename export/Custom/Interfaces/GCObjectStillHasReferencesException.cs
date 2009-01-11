using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom.Interfaces
{
	public class GCObjectStillHasReferencesException   :ApplicationException 
	{
		public object Source;

		        public GCObjectStillHasReferencesException()
        {         }

        public GCObjectStillHasReferencesException(string message, object theSource)
            :base(message)
				{ Source = theSource; }

		public GCObjectStillHasReferencesException(string message,object theSource, Exception inner)
            : base(message, inner)
		{ Source = theSource; }
	}
}
