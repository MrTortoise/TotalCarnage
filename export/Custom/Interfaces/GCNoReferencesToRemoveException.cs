using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom.Interfaces
{
	public class GCNoReferencesToRemoveException :ApplicationException 
	{
		public object Source;

		        public GCNoReferencesToRemoveException()
        {         }

        public GCNoReferencesToRemoveException(string message, object theSource)
            :base(message)
				{ Source = theSource; }

		public GCNoReferencesToRemoveException(string message,object theSource, Exception inner)
            : base(message, inner)
		{ Source = theSource; }
	}
}
