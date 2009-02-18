using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects.Controls
{
	/// <summary>
	/// These event args hold the root object that caused the event cascade.
	/// ultimatley
	/// </summary>
	public class GameControlEventArgs : EventArgs 
	{
		public GameControl Control;

		public GameControlEventArgs(GameControl theControl)
		{
			Control = theControl;
		}
	}
}
