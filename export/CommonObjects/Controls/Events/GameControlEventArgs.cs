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
		protected GameControl mControl;

		public GameControlEventArgs(GameControl theControl)
		{
			Control = theControl;
		}

		public GameControl Control
		{
			get { return mControl; }
			set { mControl = value; }
		}


	}
}
