using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonObjects.Components;

namespace CommonObjects.Components
{
	public class ConsoleVisibilityEventArgs : EventArgs 
	{
//		public ConsoleMenu Console;
		public bool IsActive;

		public ConsoleVisibilityEventArgs(/*ConsoleMenu theConsole,*/ bool Active)
		{
			IsActive = Active;
			//Console = theConsole;
		}
	}
}
